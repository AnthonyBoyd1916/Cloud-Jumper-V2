using UnityEngine;

public class DashCharge : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameSingleton.Instance.availableDashes++;
            Destroy(this.gameObject);
        }
        else { return; }
    }
}
