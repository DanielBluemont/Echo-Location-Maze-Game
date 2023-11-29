using System.Collections;
using UnityEngine;

namespace MazeGame.Player
{
    public class EchoLocation : MonoBehaviour
    {
        [SerializeField] private GameObject Echo;
        public float Sensibility = 100;
        public double treshhold = 3;
        private int arrLength = 64;

        private AudioSource audioSource;

        bool CanSay = true;
        void Start()     
        {    
            if(Microphone.devices.Length <= 0)    
            {    
                Debug.LogWarning("Microphone not connected!");    
            }    
            else 
            {    
                audioSource = this.GetComponent<AudioSource>();
                string device = Microphone.devices[0];
                audioSource.clip = Microphone.Start(device, true, 20, AudioSettings.outputSampleRate);
            }    
        }
        private void Update() 
        {
            if (CanSay)
            {
                EchoLocate();
                StartCoroutine(Reload());
            }
        }

        IEnumerator Reload()
        {
            CanSay = false;
            yield return new WaitForSeconds(0.12f);
            CanSay = true;
        }
        private void EchoLocate()
        {
            float loudness = GetLoudness(Microphone.GetPosition(Microphone.devices[0]), audioSource.clip) * Sensibility;
            if (loudness < treshhold)
            {
                return;
            }
        

            Echo echo = Instantiate(Echo, transform.position, Quaternion.identity).GetComponent<Echo>();
            if (echo != null)
            { 
                echo.SetSize(loudness);
            }
        }
    
        private float GetLoudness(int clipPosition, AudioClip clip)
        {   
            int startPosition = clipPosition - arrLength;

            if (startPosition < 0)
                return 0;

            float[] waveData = new float[arrLength];
        
            clip.GetData(waveData, startPosition);
        
            float loudness = 0;

            for (int i = 0; i < arrLength; i++)
            {
                loudness += Mathf.Abs(waveData[i]);
            }

            return loudness / arrLength; 
        }
   
    }
}
