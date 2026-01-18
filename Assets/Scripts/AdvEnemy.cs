using UnityEngine;

public class AdvEnemy : EnemyObj
{
    [SerializeField] private GameObject playerPosition;

    protected override void Start()
    {
        base.Start();
        bottomBound = movementType.bottomMaxium;
    }

    protected override void Update()
    {
        if (spawnDelay > 0)
        {
            spawnDelay -= Time.deltaTime;
        }
        if (spawnDelay <= 0)
        {
            attack();
        }

        if (objTransform.position.y >= bottomBound)
        {
            enemyDownMovement(speed + 1);
        }
        
        ClampPosition();

        playerPosition = spawnScript.player;
        float distance = objTransform.position.x - playerPosition.transform.position.x;
        Debug.Log(distance);

        enemySideMovement(speed, distance);
    }
    private void enemySideMovement(float speed, float direction)
    {
      Debug.Log("Moving to player X");
      this.transform.Translate(new Vector3(0, direction, 0) * speed * Time.deltaTime);
      
    }
}
