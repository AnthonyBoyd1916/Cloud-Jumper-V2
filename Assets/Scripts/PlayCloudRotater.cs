using UnityEngine;

public class PlayCloudRotater : MonoBehaviour
{
    private void OnEnable()
    {
        int scaleForThis = UnityEngine.Random.Range(1, 3);
        this.transform.rotation = Quaternion.Euler(0, UnityEngine.Random.Range(-45, 45), 180);
        this.transform.localScale = new Vector3(scaleForThis, scaleForThis, scaleForThis);
    }
}
