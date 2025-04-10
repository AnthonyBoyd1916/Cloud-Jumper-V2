using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Transform camPos;
    void Update()
    {
        transform.position = camPos.position;
    }
}
