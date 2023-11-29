using MazeGame.UI;
using UnityEngine;

namespace MazeGame.GamePlayObjects
{
    public class Key : MonoBehaviour,Iinteractable
    {   
        [SerializeField] private string _prompt;
        public string interactPrompt => _prompt;
        public void Interact(UIprompt uIprompt)
        {
            uIprompt.IncrementKeys();
            Destroy(this.gameObject);   
        }    
    }
}
