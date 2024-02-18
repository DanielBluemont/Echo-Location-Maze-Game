using MazeGame.UI;
using UnityEngine;
using System.Collections.Generic;

namespace MazeGame.GameFlow
{
    public class DisableOnPause : MonoBehaviour
    {
        [SerializeField] private List<MonoBehaviour> _script = new List<MonoBehaviour>();
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
            foreach (var script in _script) 
            { 
                script.enabled = !isPaused;
            }
        }
    }
}
