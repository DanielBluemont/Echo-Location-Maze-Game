using System.Collections;
using TMPro;
using UnityEngine;

namespace MazeGame.GamePlayObjects
{
    public class KeypadLock : MonoBehaviour
    {
        [SerializeField] private GameObject Key, Door;
        [SerializeField] private TextMeshProUGUI codeText;
        [SerializeField] private Material[] materials = new Material[4];
        [SerializeField] private Renderer[] LightBulbs = new Renderer[4];
        [SerializeField] private AudioClip _click, _error, _delete, _success;
        [SerializeField] private AudioSource _keyPadAudioSource;
        private string _dialedCode;
        private string _realCode;
    
        private void Start() 
        {
            SetText(string.Empty);
        }
        public void InitializeCode(int[] code, int[] order)
        {
            SetColorHint(order);
            foreach (int digit in code)
            {
                _realCode += digit.ToString();
            }
        }
        private void SetColorHint(int[] order)
        {
            for (int i = 0; i<4; i++)
            {
                LightBulbs[i].material = materials[order[i]];
            }
        }
        public void SetDigit(int digit)
        {
            _dialedCode += digit.ToString();
            _keyPadAudioSource.PlayOneShot(_click);
            SetText(_dialedCode);
            if (_dialedCode.Length >= 4)
            {
                StartCoroutine(ConfirmCode(1));
            }
        }
        public void DeleteDigit()
        {
            if (_dialedCode.Length > 0)
            {
                _dialedCode = _dialedCode.Remove(_dialedCode.Length-1);
                _keyPadAudioSource.PlayOneShot(_delete);
                SetText(_dialedCode);

            }
        }
        private void CheckCode()
        {
            if (_dialedCode == _realCode)
            {
                Instantiate(Key, transform.position, Quaternion.identity);
                StartCoroutine(OpenDoor());
                _keyPadAudioSource.PlayOneShot(_success);
            }
            else
            {
                _keyPadAudioSource.PlayOneShot(_error);
            }
            _dialedCode = string.Empty;
            SetText(_dialedCode);
        }
        private void SetText(string txt)
        {
            codeText.text = txt;
        }
        IEnumerator ConfirmCode(float delay)
        {
            yield return new WaitForSeconds(delay);
            CheckCode();

        }
        IEnumerator OpenDoor()
        {
            float elapsedTime = 0;
            while (elapsedTime < 3)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime/3;
                float angle = Mathf.Lerp(0, 155, t);
                Door.transform.rotation = Quaternion.Euler(0, angle, 0);
                yield return null;
            }
        }
    }
}
