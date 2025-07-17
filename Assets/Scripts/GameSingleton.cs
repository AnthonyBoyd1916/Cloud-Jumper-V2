using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.UI.Collections;
using TMPro;

public class GameSingleton : MonoBehaviour
{
    public static GameSingleton instance;

    public static GameSingleton Instance
    {
        get 
        {
            if (instance == null)
            {
                Debug.Log("No GameData Instance");
            }
            return instance;
        }
    }

    public float runTime = 0.0f;
    public TextMeshProUGUI timer;
    public TextMeshProUGUI dashDisplay;
    public TextMeshProUGUI leapDisplay;
    public int score;
    public int availableLeaps;
    //public int maxLeaps;
    public int availableDashes;
    //public int maxDashes;

    private void Awake()
    {
        instance = this;
    }

    private void FixedUpdate()
    {
        runTime += Time.deltaTime;
        timer.text = " " + runTime.ToString();
        dashDisplay.text = "Dashes: " + availableDashes.ToString();
        leapDisplay.text = "Leaps: " + availableLeaps.ToString();
    }
}
