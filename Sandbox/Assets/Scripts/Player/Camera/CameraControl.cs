using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    public Transform player;

    float rotationX;
    float rotationY;

    float mouseX;
    float mouseY;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        rotationY -= mouseX * -1;

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0f);
        player.Rotate(Vector3.up * mouseX);
    }

    void LateUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.557f, player.transform.position.z); //0.702f -- 0.231f 
    }
}
