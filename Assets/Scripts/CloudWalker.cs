using UnityEngine;

public class CloudWalker
{
    public Vector3 position, direction;
    public float directionChangeChance;

    public CloudWalker(Vector3 pos, Vector3 dir, float dirChangeChance)
    {
        position = pos;
        direction = dir;
        directionChangeChance = dirChangeChance;
    }
}
