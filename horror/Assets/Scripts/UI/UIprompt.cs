using System.Collections;
using MazeGame.GameFlow;
using MazeGame.AudioManaging;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MazeGame.UI
{
    public class UIprompt : MonoBehaviour
    {
        public delegate void OnPause(bool state);
        public static event OnPause OnPauseSwitch;
        public delegate void OnFinishing();
        public static event OnFinishing OnFinishingGame;
        const float SHOW_TIME = 1.5f;
        [SerializeField] private TextMeshProUGUI _prompt;
        [SerializeField] private TextMeshProUGUI keyText;
        [SerializeField] private Image slider;
        [SerializeField] private CanvasGroup backSlider;
        [Space]
        [SerializeField] private GameObject _gameMenu;
        [Space]
        [SerializeField] private NoteRevealer _noteRevealer;
        public bool isDisplayed = false;

        private void OnEnable()
        {
            Player.Player.OnEscPressed += SwitchPanelState;
        }

        private void OnDisable()
        {
            Player.Player.OnEscPressed -= SwitchPanelState;
        }
        private void SwitchPanelState()
        {
            _gameMenu.SetActive(!_gameMenu.activeSelf);
            OnPauseSwitch?.Invoke(_gameMenu.activeSelf);
            if (_gameMenu.activeSelf)
            {
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;
                AudioManagerClass.Instance.ToggleAmbience(false);
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1f;
                AudioManagerClass.Instance.ToggleAmbience(true);
            }
        }
        public void ContinueGame()
        {
            _gameMenu.SetActive(true);
            OnPauseSwitch?.Invoke(false);
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
            AudioManagerClass.Instance.ToggleAmbience(true);
        }
        public void SetSlider(float amount)
        {
            slider.fillAmount = amount;
            if (amount <= 0 || amount >=1)
            {
                backSlider.alpha = 0;
            }
            else
            {
                backSlider.alpha = 1;
            }
        }
        public float GetCurrenAmount()
        {
            return slider.fillAmount;
        }

        private void Start() 
        {
            DisableText();
        }

        public void SaveNote(string num, Color color)
        {
            _noteRevealer.CreateNoteUI(num, color);
        }
        //set prompt methods \/ \/ \/
        public void SetText(string prompt)
        {
            _prompt.text = prompt;
            _prompt.gameObject.SetActive(true);
            isDisplayed = true;
        }
        public void DisableText()
        {
            _prompt.gameObject.SetActive(false);
            isDisplayed = false;
        }

        //methods for key \/ \/ \/
        public void IncrementKeys()
        {
            OnFinishingGame?.Invoke();
            StartCoroutine(ChangeTransparency());
        }

        IEnumerator ChangeTransparency()
        {
            yield return new WaitForSeconds(3);
            keyText.gameObject.SetActive(true);
            RenderSettings.ambientLight = Color.white;
            yield return new WaitForSeconds(5);
            keyText.gameObject.SetActive(false);
        }
        //methods for notes \/\/\/
    
    }
}
