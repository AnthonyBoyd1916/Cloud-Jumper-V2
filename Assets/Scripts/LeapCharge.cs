using UnityEngine;

public class LeapCharge : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {           
            GameSingleton.Instance.availableLeaps++;
            Destroy(this.gameObject);
        }
        else { return; }
    }
}
