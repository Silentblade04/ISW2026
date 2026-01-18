using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] Collider2D projectile2D;
    public float timer = 5;

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerControls hitPlayer = collision.gameObject.GetComponent<PlayerControls>();
        if (hitPlayer != null)
        {
            Debug.Log("Hit the player");
            hitPlayer.damage();
        }
        else
        {
            Debug.Log("Never Mind");
            return;
        }
        Destroy(gameObject);
    }
}
