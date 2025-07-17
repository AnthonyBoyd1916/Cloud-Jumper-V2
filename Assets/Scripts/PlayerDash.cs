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
    public AudioClip dashSFX;
    AudioSource sfxPlayer;

    public void Start()
    {
        playerRb=GetComponent<Rigidbody>();
        sfxPlayer = GetComponent<AudioSource>();
        //playerMoveDirection=GetComponent<Transform>();
    }

    public void Update()
    {
        if (Input.GetKeyUp(dashKey) && !isDashing)
        {
            sfxPlayer.PlayOneShot(dashSFX);
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
        yield return new WaitForSeconds(0.3f);
        playerRb.mass = 1f;
        yield return new WaitForSeconds(timeBetweenDashes);        
        isDashing = false;
    }
}
