using UnityEngine;
using System.Collections;

public class LeapScript : MonoBehaviour
{
    public KeyCode leapKey;
    public int leapRemaining;
    public float leapForce, timeBetweenLeaps;
    public bool isLeaping;
    public Transform playerMoveDirection;
    private Rigidbody playerRb;

    public void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        //playerMoveDirection=GetComponent<Transform>();
    }

    public void Update()
    {
        if (Input.GetKeyUp(leapKey) && !isLeaping && leapRemaining >= 1)
        {
            isLeaping = true;
            StartCoroutine(LeapUp());
        }
        else if ( Input.GetKeyUp(leapKey) && !isLeaping && leapRemaining <= 0 ) { return; } 
        else { return; }
    }
    
    IEnumerator LeapUp()
    {
        Vector3 leapDirection;
        leapDirection = playerMoveDirection.up;
        playerRb.mass = 0.1f;
        playerRb.AddForce(leapDirection.normalized * leapForce, ForceMode.VelocityChange);
        leapRemaining--;
        yield return new WaitForSeconds(timeBetweenLeaps);
        playerRb.mass = 1f;
        isLeaping = false;
    }
}
