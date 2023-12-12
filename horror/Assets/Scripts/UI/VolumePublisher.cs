using MazeGame.AudioManaging;
using UnityEngine;

namespace MazeGame.UI
{
    public class VolumePublisher : SliderPublisher
    {
        public override void Awake()
        {
            base.Awake();
            _slider.onValueChanged.AddListener(val => AudioManagerClass.Instance.ChangeVolume(val));
        }
        public override void SetSliderValue(int value)
        {
            if (_slider != null)
            {
                _slider.value = value;
                if (AudioManagerClass.Instance != null)
                {
                    AudioManagerClass.Instance.ChangeVolume(value);
                    _sliderValue.ChangeValue(value);
                }
            }
        }
    }
}
