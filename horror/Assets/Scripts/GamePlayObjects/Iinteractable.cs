using MazeGame.UI;

namespace MazeGame.GamePlayObjects
{
    interface Iinteractable
    {   
        public string interactPrompt {get;}
        void Interact(UIprompt uIprompt);
    }
}
