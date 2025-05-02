using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform target;
    private int wavepointIndex = 0;

    [Header("Movement")]
    public float speed = 5f;
    private float originalSpeed;
    private float slowMultiplier = 1f;

    [Header("Enemy Stats")]
    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        originalSpeed = speed;
        currentHealth = maxHealth;
        target = Waypoints_script.points[0];
    }

    void Update()
    {
        float currentSpeed = originalSpeed * slowMultiplier;
        MoveToNextWaypoint(currentSpeed);

        // Reset slow multiplier at end of frame
        slowMultiplier = 1f;
    }

    void MoveToNextWaypoint(float currentSpeed)
    {
        Vector3 dir = target.position - transform.position;
        dir.y = 0f; // Ensure we only rotate on the horizontal plane

        // 🔁 Rotate to face direction
        if (dir != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        transform.Translate(dir.normalized * currentSpeed * Time.deltaTime, Space.World);

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
            GameManager.instance.EnemyReachedEnd();
            Destroy(gameObject);
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
        PlayerStats.instance.EarnMoney(5);
        Destroy(gameObject);
    }

    public void ApplySlow(float factor)
    {
        slowMultiplier = Mathf.Min(slowMultiplier, factor);
    }
}
