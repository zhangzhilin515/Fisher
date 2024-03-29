using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        float fieldView = Camera.main.fieldOfView;
        fieldView -= Input.GetAxis("Mouse ScrollWheel") * 1f;
        fieldView = Mathf.Clamp(fieldView, 1, 10);
        Camera.main.fieldOfView = fieldView;
    }
}
