using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
