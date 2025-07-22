using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class RunComplete : MonoBehaviour
{
    public int scoreIncrement;
    private int finalScore;
    public float finalTime;
    //public float displayTime;
    //public bool runFinished;
    public GameObject scoreDisplay;
    public TextMeshProUGUI scoreResult;
    
    private void Start()
    {
        scoreDisplay.SetActive(false);
        //runFinished = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            finalTime = GameSingleton.Instance.runTime;
            finalScore = scoreIncrement * (Convert.ToInt32(120.0f - finalTime));
            scoreDisplay.SetActive(true);
            scoreResult.text = "" + finalScore.ToString() + "";

            if (PlayerPrefs.HasKey("CurrentHighScore"))
            {
                if (finalScore > PlayerPrefs.GetInt("CurrentHighScore"))
                {
                    PlayerPrefs.SetInt("CurrentHighScore", finalScore);
                }
            }
            else { PlayerPrefs.SetInt("CurrentHighScore", finalScore); }

            Invoke(nameof(BackToMenu), 3f);
        }
        else { return; }
    }
    /*public void Update()
    {
        if (runFinished == true && displayTime >= -0.01f)
        {
            displayTime -= Time.deltaTime;
        }

        if (displayTime <= 0f)
        {

        }
    }*/

    public void BackToMenu()
    {
        SceneManager.LoadScene("CloudJumperMenu");
    }
}
