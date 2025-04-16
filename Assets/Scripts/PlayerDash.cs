using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public KeyCode dashKey;
    public int dashRemaining;
    public float dashForce, timeBetweenDashes;
    public bool isDashing;
    public Transform playerMoveDirection;
    private Rigidbody playerRb;

    public void Start()
    {
        playerRb=GetComponent<Rigidbody>();
        //playerMoveDirection=GetComponent<Transform>();
    }

    public void Update()
    {
        if (Input.GetKeyUp(dashKey) && !isDashing)
        {
            isDashing = true;
            StartCoroutine(DashForward());
        }
        else { return; }
    }

    IEnumerator DashForward()
    {
        Vector3 dashDirection;
        dashDirection = playerMoveDirection.forward;
        playerRb.mass = 0.1f;
        playerRb.AddForce(dashDirection.normalized * dashForce, ForceMode.VelocityChange);
        dashRemaining--;
        yield return new WaitForSeconds(timeBetweenDashes);
        playerRb.mass = 1f;
        isDashing = false;
    }
}
