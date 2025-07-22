using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    
    public void StartButtonClick()
    {
        SceneManager.LoadScene("CloudJumperMain");
    }
    public void ExitButtonClick()
    {
        Application.Quit();

#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;

#endif
    }
}
