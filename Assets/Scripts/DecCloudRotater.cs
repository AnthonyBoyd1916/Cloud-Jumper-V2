using UnityEngine;
using System;


public class DecCloudRotater : MonoBehaviour
{
    private void OnEnable()
    {
        int scaleForThis = UnityEngine.Random.Range(3 , 20);
        this.transform.rotation = Quaternion.Euler(UnityEngine.Random.Range(-45, 45), UnityEngine.Random.Range(-45, 45), 0);
        this.transform.localScale = new Vector3(scaleForThis,scaleForThis,scaleForThis);
    }
}
