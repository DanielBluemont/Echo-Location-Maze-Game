using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MazeGame
{
    public class MicSelector : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _dropdown;
        [SerializeField] private Image _audioBar;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private int Sensibility = 1, MinSens = 1, MaxSense = 50;
        public double treshhold = 3;
        private int sampleWindow = 64;
        [SerializeField] int _index;

        public static UnityAction<int> OnMicChanged;

        private void Awake()
        {
            FillDropDown();
            _dropdown.value = PlayerPrefs.GetInt("MicOption", 0);
        }
        private void Start()
        {
            ConnectMic(PlayerPrefs.GetInt("MicOption", 0));
        }
        private void ConnectMic(int index)
        {
            if (Microphone.devices.Length <= 0)
            {
                Debug.LogWarning("Microphone's not connected!");
            }
            else
            {
                _index= index;
                Debug.Log(Microphone.devices[_index]);
                
                audioSource.clip = Microphone.Start(Microphone.devices[_index], true, 20, AudioSettings.outputSampleRate);
            }
        }
        private void Update()
        {
            float loudness = GetLoudness(Microphone.GetPosition(Microphone.devices[_index]), audioSource.clip) * Sensibility;

            float mappedLoudness = MapValue(loudness, 0, 1, MinSens, MaxSense);
            Debug.Log($" lol {mappedLoudness}, uu {loudness}");
            if (mappedLoudness < treshhold)
            {
                mappedLoudness = 0.01f;
            }

            float normalizedLoudness = (mappedLoudness - MinSens) / (MaxSense - MinSens);

            _audioBar.fillAmount = normalizedLoudness;
        }

        public void SetSensibility(float t)
        {
            Sensibility = (int)Mathf.Lerp(MinSens, MaxSense, t);
        }
        private float MapValue(float value, float fromSource, float toSource, float fromTarget, float toTarget)
        {
            return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
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
        private void FillDropDown()
        {
            var options = new List<TMP_Dropdown.OptionData>();

            foreach (var mic  in Microphone.devices) 
            { 
                TMP_Dropdown.OptionData data = new TMP_Dropdown.OptionData(mic, null);
                options.Add(data);
            }

            _dropdown.options = options;
        }

        public void ChooseMic(int index)
        {
            ConnectMic(index);
            //OnMicChanged?.Invoke(index);
            PlayerPrefs.SetInt("MicOption", index);
        }
    }
}
