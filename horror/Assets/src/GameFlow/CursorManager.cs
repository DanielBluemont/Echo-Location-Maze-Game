using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private bool isLocked;
    private void Start() 
    {   
        if (isLocked) Cursor.lockState = CursorLockMode.Locked;
        else Cursor.lockState = CursorLockMode.None;    
    }
}
