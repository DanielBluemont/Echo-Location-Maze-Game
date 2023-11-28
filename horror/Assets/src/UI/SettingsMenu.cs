using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private Slider _volumeSlider, _senseSlider;
    private void Start()
    {
        LoadSettings();
    }

    public void CloseSettings()
    {
        _settingsPanel?.SetActive(false);
        SetSoundVolume((int)_volumeSlider.value);
        SetSenseValue((int)_senseSlider.value);
        Debug.Log($"SETTINGS ARE SAVED. \n " +
            $"VOLUME IS: {PlayerPrefs.GetInt("SoundVolume")} \n" +
            $"SENSE IS : {PlayerPrefs.GetInt("SenseValue")}");
        SaveSettings(); 
    }

    public void SetSoundVolume(int _soundVolume)
    {
        PlayerPrefs.SetInt("SoundVolume", _soundVolume);
    }

    public void SetSenseValue(int _senseValue)
    {
        PlayerPrefs.SetInt("SenseValue", _senseValue);
    }


    private void LoadSettings()
    {
        _volumeSlider.GetComponent<SliderPublisher>().
            SetSliderValue(PlayerPrefs.GetInt("SoundVolume", 100));
        _senseSlider.GetComponent<SliderPublisher>().
            SetSliderValue(PlayerPrefs.GetInt("SenseValue", 100));
    }
    private void SaveSettings()
    {
        PlayerPrefs.Save();
    }
}
