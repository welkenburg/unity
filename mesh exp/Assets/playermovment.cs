using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovment : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 30f;
    public float mouseSensitivity = 100f;

    public float xoff = 0f;
    public float yoff = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yoff += mouseX;
        xoff += mouseY;
        transform.eulerAngles = new Vector3(-xoff, yoff, 0);
    }
}
/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovment : MonoBehaviour
{
    CharacterController controller;
    float mouseSensitivity = 10f;
    float xRotation = 0f;
    float zRotation = 0f;
    float speed = 20f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float x = 0;
        float y = 0;
        float z = 0;
        if (Input.GetKeyDown(KeyCode.Z)) x = speed;
        if (Input.GetKeyDown(KeyCode.S)) x = speed;
        if (Input.GetKeyDown(KeyCode.Space)) y += speed;
        if (Input.GetKeyDown(KeyCode.W)) y -= speed;
        if (Input.GetKeyDown(KeyCode.D)) z += speed;
        if (Input.GetKeyDown(KeyCode.Q)) z -= speed;
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.position += new Vector3(x, y, z) * Time.deltaTime;

    }
}*/
