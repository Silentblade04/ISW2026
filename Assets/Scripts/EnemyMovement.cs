using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScritableObjects/Enemy Movement", order = 1)]
public class EnemyMovement : ScriptableObject
{
    public string movementType;
    
    public float movementSpeed;

    public float leftMaximum;
    public float rightMaximum;

    public float bottomMaxium;

    public int score;

    public float fd;
}
