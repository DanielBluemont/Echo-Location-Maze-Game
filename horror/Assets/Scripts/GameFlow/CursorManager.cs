using UnityEngine;

namespace MazeGame.GameFlow
{
    public class CursorManager : MonoBehaviour
    {
        [SerializeField] private bool isLocked;
        private void Start() 
        {   
            if (isLocked) Cursor.lockState = CursorLockMode.Locked;
            else Cursor.lockState = CursorLockMode.None;    
        }
    }
}
