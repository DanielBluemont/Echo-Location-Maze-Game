using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Interaction : MonoBehaviour
{
    
    [SerializeField] private LayerMask canInteract, canLift;
    [SerializeField] private Transform PickUpPoint;
    [Space]
    [SerializeField] private float range, speed, throwForce, radius;
    [Space]
    [SerializeField] private UIprompt UIprompt;
        
    private readonly Collider[] colliders = new Collider[3];
    private int found;
    private Rigidbody currentObj;
    private Camera cam;
    private Iinteractable _interactable;  
    
    
    private void Start() 
    {
        cam = GetComponentInChildren<Camera>();
        UIprompt = FindObjectOfType<UIprompt>();
    }
    private void Update() 
    {
        Interact();
        Throw();
        PickUp();
    }
    private void PickUp()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (currentObj)
            {
                currentObj.useGravity = true;
                currentObj = null;
                return;
            }
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(ray, out RaycastHit HitInfo, range, canLift))
            {   
                Button button = HitInfo.collider.GetComponent<Button>();
                if (button != null)
                {
                    button.onClick.Invoke();
                    return;
                }
                currentObj = HitInfo.rigidbody;

                currentObj.useGravity = false;
                
            }
        }
    }
    private void Interact()
    {
        found = Physics.OverlapSphereNonAlloc(transform.position, radius, colliders, canInteract);

        if (found > 0)
        {
            _interactable = colliders[0].GetComponent<Iinteractable>();
        
            if (_interactable != null)
            {
                if (!UIprompt.isDisplayed) UIprompt.SetText(_interactable.interactPrompt);
                if (Input.GetKeyDown(KeyCode.E)) _interactable.Interact(UIprompt);
            }
        }
        else
        {
            if (_interactable != null) _interactable = null;
            if (UIprompt.isDisplayed) UIprompt.DisableText();
        }
    }
    
    
    
    private void Throw()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (currentObj == null) return;
            currentObj.useGravity = true;
            currentObj.AddForce(transform.forward * throwForce, ForceMode.Impulse);
            currentObj = null;
        }
    }
    private void FixedUpdate()
    {
        if (currentObj)
        {
            Vector3 direction = PickUpPoint.position - currentObj.position;
            float distance = direction.magnitude;
            currentObj.velocity = direction * distance * speed; 
        }
    }
}
