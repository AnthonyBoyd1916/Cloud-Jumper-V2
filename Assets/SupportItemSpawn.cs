using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class SupportItemSpawn : MonoBehaviour
{
    private bool isDash, isLeap;
    public GameObject dashCharge;
    public GameObject leapCharge;
    private Vector3 spawnPosition;

    void Awake()
    {
        spawnPosition = this.gameObject.transform.position;
        int SpawnChance = Convert.ToInt32(UnityEngine.Random.Range(1f, 10f));
        if (SpawnChance >= 8)
        {
            Debug.Log("CHARGE SPAWNING");
            int DashorLeap = Convert.ToInt32(UnityEngine.Random.Range(0f, 2f));
            if ((DashorLeap > 1))
            {
                isDash = true;
                isLeap = false;               
            }
            else if ((DashorLeap <= 1))
            {
                isDash = false;
                isLeap = true;
            }
            else { return; }
        }
    }

    void Start()
    {
        if (isDash)
        {
            SpawnDash(spawnPosition);
        }
        else if (isLeap)
        {
            SpawnLeap(spawnPosition);
        }
    }

    public void SpawnDash(Vector3 dashSpawn)
    {
        Instantiate(dashCharge, dashSpawn, Quaternion.identity);
    }

    public void SpawnLeap(Vector3 leapSpawn)
    {
        Instantiate(leapCharge, leapSpawn, Quaternion.identity);
    }
}
