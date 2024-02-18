using System.Collections;
using UnityEngine;
using MazeGame.EchoParticleSystem;
using MazeGame.EnemyAI;
using System.Collections.Generic;
using MazeGame.UI;

namespace MazeGame.Player
{

    public class EchoLocation : MonoBehaviour
    {
        
        [SerializeField] private GameObject Echo;
        [SerializeField] private int Sensibility = 100, MinSens = 100, MaxSense = 1000;
        [SerializeField] private ParticleSystem _ps;
        public double treshhold = 3;
        private int sampleWindow = 64;

        private AudioSource audioSource;
        private MonsterListener _ml;
        private string _micName;

        List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();

        bool CanSay = true;

        /*private void OnEnable()
        {
            MicSelector.OnMicChanged += ConnectMic;
            StartRecording();
        }

        private void OnDisable()
        {
            StopRecording();
            MicSelector.OnMicChanged -= ConnectMic;
        }*/
        private void OnEnable()
        {
            ConnectMic(PlayerPrefs.GetInt("MicOption", 0));
        }
       
        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            _ml = FindObjectOfType<MonsterListener>();
            _ml.SetRef(transform.parent.GetComponent<Player>());
            _ps.trigger.AddCollider(_ml.GetComponent<Collider>());
            
        }
        
        private void ConnectMic(int index)
        {
            _micName = Microphone.devices[index];
            Debug.Log(_micName);
            StartRecording();
        }
        private void StartRecording()
        {
            audioSource.clip = Microphone.Start(_micName, true, 20, AudioSettings.outputSampleRate);
        }
        private void StopRecording()
        {
            if (audioSource.isPlaying) 
            { 
                Microphone.End(_micName);
                audioSource.Stop();
            }
        }
        
        private void EchoLocate()
        {
            float loudness = GetLoudness(Microphone.GetPosition(_micName), audioSource.clip) * Sensibility;
            Debug.Log($"current {loudness}");
            if (loudness < treshhold)
            {
                return;
            }
        
            EmitSphere(loudness);
        }
        private void Update() 
        {
            if (CanSay)
            {
                EchoLocate();
                StartCoroutine(Reload());
            }
        }
        
        public void ChangeSense(float value)
        {
            Sensibility = (int)Mathf.Lerp(MinSens, MaxSense, value);
        }
        IEnumerator Reload()
        {
            CanSay = false;
            yield return new WaitForSeconds(0.12f);
            CanSay = true;
        }
        

        private IEnumerator ColorFlash()
        {
            yield return new WaitForSeconds(0.5f);
        }
        private void EmitSphere(float loud)
        {
            loud = Mathf.Clamp(loud *3, 1, 40);
            float time = ParticleManager.SLOPE * loud;
            
            var emitParams = new ParticleSystem.EmitParams();
            emitParams.startLifetime = time;
            emitParams.startSize = loud;
            
            _ps.Emit(emitParams, 1);

            ParticleManager.Instance.CreateSound(transform.position, loud);
        }
        private void OnParticleTrigger()
        {
            int num = _ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
            for (int i = 0; i < num; i++) 
            {
                ParticleSystem.Particle p = enter[i];
                p.startColor = new Color32(255, 0, 0, 255);
                enter[i] = p;
            }

            _ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
            
            /*StartCoroutine(ColorFlash());

            for (int i = 0; i < num; i++)
            {
                ParticleSystem.Particle p = enter[i];
                p.startColor = ParticleManager.Instance.colorOr;
                enter[i] = p;
            }

            _ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
            Debug.Log("Works");*/
        }

        private float GetLoudness(int clipPosition, AudioClip clip)
        {   
            int startPosition = clipPosition - sampleWindow;

            if (startPosition < 0)
                return 0;

            float[] waveData = new float[sampleWindow];
        
            clip.GetData(waveData, startPosition);
        
            float loudness = 0;

            for (int i = 0; i < sampleWindow; i++)
            {
                loudness += Mathf.Abs(waveData[i]);
            }

            return loudness / sampleWindow; 
        }
   
    }
}
