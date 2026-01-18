using System.Collections;
using UnityEngine;

public class BurstEnemy : EnemyObj
{
    [SerializeField] private float burstCooldown;
    [SerializeField] private float burstDelay;
    [SerializeField] private float burstShot;

    [SerializeField] private bool bursting;

    protected override void Update()
    {
        base.Update();
    }
    protected override void attack()
    {
        if (!bursting)
        {
            StartCoroutine(BurstFire());
        }
    }
    private IEnumerator BurstFire()
    {
        bursting = true;

        for (int i = 0; i < burstShot; i++)
        {
            GameObject projectile = Instantiate(
                projectileBody,
                new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z),
                Quaternion.identity
            );

            projectile.GetComponent<Rigidbody2D>().linearVelocityY = -2f;
            yield return new WaitForSeconds(burstDelay);
        }

        // Cooldown after burst
        yield return new WaitForSeconds(burstCooldown);
        bursting = false;
    }
}
