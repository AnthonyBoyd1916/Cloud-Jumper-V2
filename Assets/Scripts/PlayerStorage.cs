using UnityEngine;

public class PlayerStorage : MonoBehaviour
{
    public static PlayerStorage instance { get; private set; }

    public int playerLives;
    public int playerCurrentScore;
    public Time playerCurrentTime;
    public int currentDashes;
    public int currentLeaps;
}
