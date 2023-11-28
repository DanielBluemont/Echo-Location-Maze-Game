using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface Iinteractable
{   
    public string interactPrompt {get;}
    void Interact(UIprompt uIprompt);
}
