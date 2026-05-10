using UnityEngine;

public class MissileEnemy : AdvEnemy
{
    protected override void attack()
    {
        Debug.Log("Missile Enemy Attacking");
        GameObject projectile = Instantiate(projectileBody, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), Quaternion.identity);
        spawnDelay = Random.Range(movementType.fd - 1, movementType.fd + 1);
    }
}
