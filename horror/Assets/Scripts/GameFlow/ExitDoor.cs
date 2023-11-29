using MazeGame.GamePlayObjects;
using MazeGame.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MazeGame.GameFlow
{
    public class ExitDoor : MonoBehaviour, Iinteractable
    {
        [SerializeField] private string _prompt;
        public string interactPrompt => _prompt;
        private bool lockstate = false;

        public void Unlock()
        {
            lockstate = true;
        }

        public void Interact(UIprompt uIprompt)
        {
            if (lockstate)
            {
                SceneManager.LoadScene(2);
            }
        } 
    }
}
