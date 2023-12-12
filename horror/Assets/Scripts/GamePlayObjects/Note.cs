using MazeGame.UI;
using TMPro;
using UnityEngine;

namespace MazeGame.GamePlayObjects
{
    public class Note : MonoBehaviour, Iinteractable
    {
        [SerializeField] private string _prompt;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Color _color;
        public void SetDigit(int digit)
        {
            text.text = digit.ToString();
        }
        public string interactPrompt => _prompt;

        public void Interact(UIprompt uIprompt)
        {
            uIprompt.SaveNote(text.text, _color);
            Destroy(this.gameObject);
        }
    
    }
}
