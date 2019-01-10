using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   
    public enum RotationAxis { mouseX = 1, mouseY = 2 }

    public RotationAxis axes = RotationAxis.mouseX;

    public float minVertical = -45.0f;
    public float maxVertical = 45.0f;

    public float sensHorizontal = 10.0f;
    public float sensVertical = 10.0f;

    public float rotationX = 0;

    // Update is called once per frame
    void Update()
    {
        if(axes == RotationAxis.mouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensHorizontal, 0);
        } else if(axes == RotationAxis.mouseY)
        {
            rotationX -= Input.GetAxis("Mouse Y") * sensVertical;
            rotationX = Mathf.Clamp(rotationX, minVertical, maxVertical);

            float rotationY = transform.localEulerAngles.y;

            transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
        }

    }
}
