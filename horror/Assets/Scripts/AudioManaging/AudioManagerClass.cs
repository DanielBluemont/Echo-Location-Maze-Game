using UnityEngine;
using UnityEngine.SceneManagement;

namespace MazeGame.AudioManaging
{
    public class AudioManagerClass : MonoBehaviour
    {
        public static AudioManagerClass Instance;

        [SerializeField] private AudioSource _ambience, _effects;

        [SerializeField] private AudioClip _menuClip, _gameClip;
        [Space]
        [SerializeField] private SoundEffect _soundEffect, _soundEffect3D;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        public void ToggleAmbience(bool isOn)
        {
            if (isOn) _ambience.UnPause();
            else _ambience.Pause();
        }
        private void ChangeSceneAmbience(Scene scene, LoadSceneMode mode)
        {
            int index = SceneManager.GetActiveScene().buildIndex;
            if (index == 1)
                SetAmbience(_gameClip);
            else
                SetAmbience(_menuClip);
        }
        private void SetAmbience(AudioClip _clip)
        {
            _ambience.clip = _clip;
            _ambience.Play();
        }


        public void ChangeVolume(float value)
        {
            AudioListener.volume = value/100;
        }



        private void OnEnable()
        {
            SceneManager.sceneLoaded += ChangeSceneAmbience;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= ChangeSceneAmbience;

        }
    }
}
