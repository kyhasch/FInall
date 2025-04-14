using UnityEngine;

public class Slime_Tower : MonoBehaviour
{
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public float range = 10f; // Range within which the tower will shoot
    public float fireRate = 1f; // Time between shots
    private float fireCountdown = 0f;

    void Update()
    {
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        // Find the closest enemy
        GameObject targetEnemy = FindClosestEnemy();
        if (targetEnemy != null)
        {
            // Instantiate a projectile and set its target
            GameObject projectileGO = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Projectile projectile = projectileGO.GetComponent<Projectile>();

            if (projectile != null)
            {
                projectile.Seek(targetEnemy.transform);
            }
        }
    }

    GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance && distanceToEnemy <= range)
            {
                shortestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }
}
