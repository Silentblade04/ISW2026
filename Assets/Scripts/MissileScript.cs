using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MissileScript : MonoBehaviour
{
    [SerializeField] Collider2D projectile2D;

    [SerializeField] private float speed = 2f;
    [SerializeField] private float rotationSpeed = 200f;

    [SerializeField] private Transform playerTarget;


    public float timer = 5;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTarget = player.transform;
        }

    }


    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0 || gameObject.transform.position.y <= -4)
        {
            Destroy(gameObject);
        }

        if (playerTarget == null) return;

        Vector2 direction = (Vector2)playerTarget.position - (Vector2)transform.position;
        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        transform.Rotate(0, 0, -rotateAmount * rotationSpeed * Time.deltaTime);

        transform.Translate(Vector2.up * speed * Time.deltaTime);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        PlayerControls hitPlayer = collision.gameObject.GetComponent<PlayerControls>();
        if (hitPlayer == null)
        {
            Debug.Log("Never Mind");
            return;
        }

        Debug.Log("Hit the player");
        hitPlayer.damage(3);

        Destroy(gameObject);
    }
}
