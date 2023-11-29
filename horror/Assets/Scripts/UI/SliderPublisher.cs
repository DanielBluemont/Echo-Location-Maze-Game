using MazeGame.Sound;
using UnityEngine;
using UnityEngine.UI;

namespace MazeGame.UI
{
    public abstract class SliderPublisher : MonoBehaviour
    {
        [SerializeField] protected VolumeSliderValue _sliderValue;
        [SerializeField] protected Slider _slider;
        public virtual void Awake()
        {
            _slider.onValueChanged.AddListener(val => _sliderValue.ChangeValue((int)val));
        }

        public virtual void SetSliderValue(int value)
        {
            if (_slider != null)
            {
                _slider.value = value;
                if (AudioManagerClass.Instance != null)
                {
                    AudioManagerClass.Instance.ChangeVolume(value);
                    _sliderValue.ChangeValue(value);
                    Debug.Log($"Value is Changed to {value}");
                }
                else
                {
                    Debug.LogError("AudioManagerClass.Instance is null!");
                }
            }
            else
            {
                Debug.LogError("_slider is null!");
            }
        }


    }
}
