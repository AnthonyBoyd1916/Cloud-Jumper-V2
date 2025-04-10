using UnityEngine;

public class CameraPointer : MonoBehaviour
{
    public float detectX, detectY;
    public Transform orientation;
    float rotateX, rotateY;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * detectX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * detectY;

        rotateY += mouseX;
        rotateX -= mouseY;
        rotateX = Mathf.Clamp(rotateX, -90f, 90f);

        transform.rotation = Quaternion.Euler(rotateX, rotateY, 0);
        orientation.rotation = Quaternion.Euler(0, rotateY, 0);
    }
}
