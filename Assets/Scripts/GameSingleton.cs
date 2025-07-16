using UnityEngine;
using System.Collections;

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
    public int score;
    public int availableLeaps;
    public int maxLeaps;
    public int availableDashes;
    public int maxDashes;

    private void Awake()
    {
        instance = this;
    }


}
