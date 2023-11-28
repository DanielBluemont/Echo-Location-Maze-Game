using UnityEngine;

public class DisableOnPause : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _script;
    private void OnEnable()
    {
        UIprompt.OnPauseSwitch += ToggleState;
    }

    private void OnDisable()
    {
        UIprompt.OnPauseSwitch -= ToggleState;
    }

    private void ToggleState(bool isPaused)
    {
        _script.enabled = !isPaused;
    }
}
