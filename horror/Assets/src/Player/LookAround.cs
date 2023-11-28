using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAround : MonoBehaviour
{
    public float Sensetivity = 100f;
    Transform CameraTransform;
    float r = 0;
    private void Awake() 
    {
        CameraTransform = GetComponentInChildren<Camera>().GetComponent<Transform>();    
    }
    
    private void Update() 
    {
        RotateCamera();
    }
    private void RotateCamera()
    {
        float x = Input.GetAxis("Mouse X") * Sensetivity * Time.fixedDeltaTime;
        float y = Input.GetAxis("Mouse Y") * Sensetivity * Time.fixedDeltaTime;

        r -= y;
        r = Mathf.Clamp(r, -90f, 90f);
        
        CameraTransform.localRotation = Quaternion.Euler(r, 0f, 0f);
        transform.Rotate(Vector3.up * x); 
    }
}
