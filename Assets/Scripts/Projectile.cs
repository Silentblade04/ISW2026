using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScritableObjects/Projectile", order = 1)]
public class Projectile : ScriptableObject
{
    public string projectileName;

    public int numberOfShots;
    public Vector2[] spawnPoints;

    public float timer;
}
