using System.Collections.Generic;
using UnityEngine;

public class SpiderTower : MonoBehaviour
{
    [Header("Tower Settings")]
    public float range = 10f;
    public float fireRate = 1f;
    public float slowFactor = 0.5f;

    [Header("Projectile Settings")]
    public GameObject projectilePrefab;

    private float fireCountdown = 0f;

    void Update()
    {
        RotateTowardEnemy();
        // Slowing happens to all enemies in range
        ApplySlowToEnemiesInRange();

        // Handle shooting logic
        fireCountdown -= Time.deltaTime;
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
    }

    void Shoot()
    {
        GameObject targetEnemy = FindClosestEnemy();
        if (targetEnemy != null)
        {
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
    void RotateTowardEnemy()
{
    GameObject targetEnemy = FindClosestEnemy();
    if (targetEnemy != null)
    {
        Vector3 direction = targetEnemy.transform.position - transform.position;
        direction.y = 0f; // Keep tower rotation horizontal (no tilt)
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // smooth turn
    }
}


    void ApplySlowToEnemiesInRange()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemyObj in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemyObj.transform.position);
            if (dist <= range)
            {
                Enemy enemy = enemyObj.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.ApplySlow(slowFactor);
                }
            }
        }
    }

    // Optional: draw range in editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}



