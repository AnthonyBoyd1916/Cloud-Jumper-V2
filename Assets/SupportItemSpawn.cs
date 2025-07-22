using UnityEngine;

public class SupportItemSpawn : MonoBehaviour
{
    private bool isDash, isLeap;

    void Start()
    {
        int SpawnChance = Convert.ToInt32(UnityEngine.Random.Range(1f, 20f));
        if (SpawnChance > 19)
        {
            int DashorLeap = Convert.ToInt32(UnityEngine.Random.Range(0f, 2f));
            if ((DashorLeap > 1)
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isDash)
            {
                GameSingleton.Instance.availableDashes++;
                isDash = false;
                this.gameObject.Destroy();
            }
            else if (isLeap)
            {
                GameSingleton.Instance.availableLeaps++;
                isLeap = false;
            }
        }
        else { return; }
    }
}
