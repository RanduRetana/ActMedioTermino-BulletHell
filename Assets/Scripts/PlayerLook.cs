using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera cam;
    private float xRotation = 0.0f;
    public float mouseSensitivity = 8.0f;

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        xRotation -= (mouseY * mouseSensitivity * Time.deltaTime);
        xRotation = Mathf.Clamp(xRotation, -80.0f, 80.0f);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);
        transform.Rotate(Vector3.up * (mouseX * mouseSensitivity * Time.deltaTime));
    }
}
