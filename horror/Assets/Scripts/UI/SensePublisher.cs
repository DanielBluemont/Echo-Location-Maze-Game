using MazeGame.AudioManaging;
using MazeGame.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MazeGame.UI
{
    public class SensePublisher : SliderPublisher
    {
        private EchoLocation refToEchoLoc;
        public void Awake()
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                refToEchoLoc = FindObjectOfType<EchoLocation>();
                _slider.onValueChanged.AddListener(val => refToEchoLoc.ChangeSense(val));
            }
        }
        public override void SetSliderValue(float value)
        {
            if (_slider != null)
            {
                _slider.value = value;
                if (refToEchoLoc != null)
                {
                    refToEchoLoc.ChangeSense(value);
                    Debug.Log($"Value is Changed to {value}");
                }
            }
        }
    }
}
