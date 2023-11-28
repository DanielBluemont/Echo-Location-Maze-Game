using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensePublisher : SliderPublisher
{
    public override void SetSliderValue(int value)
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
