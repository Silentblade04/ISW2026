using UnityEngine;

public class EnemyObj : MonoBehaviour
{
    public int CollumNumber { get { return collumNumber; } }

    [SerializeField] protected int score;
    [SerializeField] protected EnemyMovement movementType;

    [SerializeField] protected float speed;
    [SerializeField] protected float leftBound;
    [SerializeField] protected float rightBound;

    [SerializeField] protected int direction;

    [SerializeField] protected float bottomBound;

    [SerializeField] protected Transform objTransform;

    [SerializeField] protected int Health;

    [SerializeField] protected GameObject projectileBody;

    [SerializeField] protected float spawnDelay;


    protected SpawnerScript spawnScript;

    protected int collumNumber;

    protected virtual void Start()
    {
        speed = movementType.movementSpeed;
        leftBound = movementType.leftMaximum;
        rightBound = movementType.rightMaximum;

        spawnDelay = movementType.fd;

        this.score = movementType.score;

        objTransform = transform;

        Health = 1;
    }

    protected virtual void Update()
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
            enemyDownMovement(speed+1);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, bottomBound, 0);
        }
            ClampPosition();
        
    }

    public void spawnedPosition(int minionNumber)
    {
        bottomBound = -1+minionNumber;
    }

    public void offSet()
    {
        bottomBound -= 1;
    }
    
    public void initalized(SpawnerScript spawnerScript, int collumIndex, int collumNumber) //collumIndex is the position in the collum, collumNumber is the collum
    {
        spawnScript = spawnerScript;
        transform.position = new Vector3(-8.5f+(collumNumber/10f)*17f, 5, 0);

        this.collumNumber = collumNumber;

        bottomBound = movementType.bottomMaxium;
        bottomBound += collumIndex;
    }

    public void damage()
    {
        Debug.Log("Enemy Was Hit");
        Health -= 1;
        if (Health <= 0)
        {
            spawnScript.increaseScore(score);
            spawnScript.killEnemy(gameObject, collumNumber);
        }
        
    }

    protected virtual void attack()
    {
        Debug.Log("Enemy Attacking");
        GameObject projectile = Instantiate(projectileBody, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().linearVelocityY = -2f;
        spawnDelay = Random.Range(movementType.fd - 1,movementType.fd +1);
    }

    protected void enemyDownMovement(float speed)
    {
        objTransform.Translate(Vector2.left *speed*Time.deltaTime);
    }

    protected void ClampPosition()
    {
        Vector2 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, leftBound, rightBound);

        transform.position = pos;
    }
}
