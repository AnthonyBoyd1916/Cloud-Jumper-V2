using UnityEngine;
using UnityEngine.SceneManagement;

public class RunComplete : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //SceneManager.
            SceneManager.LoadScene("CloudJumperMenu");
        }
        else { return; }
    }
}
