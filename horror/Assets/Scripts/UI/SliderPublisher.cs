using MazeGame.AudioManaging;
using UnityEngine;
using UnityEngine.UI;

namespace MazeGame.UI
{
    public abstract class SliderPublisher : MonoBehaviour
    {
        
        [SerializeField] protected Slider _slider;
        

        public virtual void SetSliderValue(float value)
        {
            if (_slider != null)
            {
                _slider.value = value;
                if (AudioManagerClass.Instance != null)
                {
                    AudioManagerClass.Instance.ChangeVolume(value);
                    
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
