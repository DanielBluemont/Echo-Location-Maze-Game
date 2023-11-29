using System.Collections;
using MazeGame.GameFlow;
using MazeGame.Sound;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MazeGame.UI
{
    public class UIprompt : MonoBehaviour
    {
        public delegate void OnPause(bool state);
        public static event OnPause OnPauseSwitch;
        const float SHOW_TIME = 1.5f;
        [SerializeField] private TextMeshProUGUI _prompt;
        [SerializeField] private TextMeshProUGUI keyText;
        [SerializeField] private RawImage keyUI;
        [SerializeField] private Exit exit;
        [SerializeField] private Image slider;
        [SerializeField] private CanvasGroup backSlider;
        [Space]
        [SerializeField] private GameObject _gameMenu;
        private int keyNum = 0;
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

        public int KeyAmount
        {
            get
            {
                return keyNum;
            }
        }
        private void Start() 
        {
            DisableText();
         
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
            if (keyNum >= 1) return;
            keyNum++;
            keyText.text = $"{keyNum}/{1}";
            StartCoroutine(ChangeTransparency());
            exit = FindObjectOfType<Exit>(); 
            exit.UnlockDoor();
        }

        IEnumerator ChangeTransparency()
        {
            float elapsedTime = 0;
            while (elapsedTime < SHOW_TIME)
            {
                elapsedTime += Time.deltaTime;
                float value = elapsedTime/SHOW_TIME;
                value = Mathf.Lerp(0,1, value);
                Color c = new Color(1, 1, 1, value);
                keyText.color = c;
                keyUI.color = c;
                yield return null;
            }
            elapsedTime = 0;
            while (elapsedTime < SHOW_TIME)
            {
                elapsedTime += Time.deltaTime;
                float value = elapsedTime/SHOW_TIME;
                value = Mathf.Lerp(1,0, value);
                Color c = new Color(1, 1, 1, value);
                keyText.color = c;
                keyUI.color = c;
                yield return null;
            }
        }
        //methods for notes \/\/\/
    
    }
}
