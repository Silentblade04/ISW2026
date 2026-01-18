using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField] Collider2D projectile2D;
    [SerializeField] Projectile projectileOBJ;

    public float timer;


    void Start()
    {
        projectile2D = GetComponent<Collider2D>();
        timer = projectileOBJ.timer;
    }

    private void Update()
    {
        //GetComponent<Rigidbody2D>().linearVelocity = Vector2.up * 10 * Time.deltaTime;
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0) 
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D (Collider2D collision)
    {
        EnemyObj hitEnemy = collision.gameObject.GetComponent<EnemyObj>();
        if (hitEnemy != null)
        {
            hitEnemy.damage();
        }
        else
        {
            return;
        }
            Destroy(gameObject);
    }
}
