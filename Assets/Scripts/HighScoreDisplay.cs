using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Collections;
using TMPro;

public class HighScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI highScoreDisplay;
    private int currentHighScore;

    void Start()
    {
        currentHighScore = PlayerPrefs.GetInt("CurrentHighScore");
        highScoreDisplay.text = "" + currentHighScore.ToString() + "";
    }
}
