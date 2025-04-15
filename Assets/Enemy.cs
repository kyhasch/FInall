using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform target;
    private int wavepointIndex = 0;
    public float speed = 5f;

    [Header("Enemy Stats")]
    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        target = Waypoints_script.points[0];
    }

    void Update()
    {
        MoveToNextWaypoint();
    }

    void MoveToNextWaypoint()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        wavepointIndex++;
        if (wavepointIndex >= Waypoints_script.points.Length)
        {
            // Notify GameManager that an enemy has reached the goal
            GameManager.instance.EnemyReachedEnd();
            Destroy(gameObject); // Reached end, destroy the enemy
            return;
        }

        target = Waypoints_script.points[wavepointIndex];
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        PlayerStats.instance.EarnMoney(5);  // Reward player for killing the enemy
        Destroy(gameObject);  // Destroy the enemy object
    }
}
