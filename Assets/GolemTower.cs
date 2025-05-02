using UnityEngine;

public class GolemTower : MonoBehaviour
{
    public float slamRange = 3f;
    public float slamDamage = 20f;
    public float slamCooldown = 2f;

    private float slamTimer = 0f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        slamTimer -= Time.deltaTime;
        RotateTowardEnemy();

        if (slamTimer <= 0f)
        {
            Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, slamRange);
            bool foundEnemy = false;

            foreach (Collider col in enemiesInRange)
            {
                if (col.GetComponent<Enemy>() != null)
                {
                    foundEnemy = true;
                    break;
                }
            }

            if (foundEnemy)
            {
                SlamAttack(enemiesInRange);
                slamTimer = slamCooldown;

                // 🔁 Trigger slam animation
                if (animator != null)
                    animator.SetTrigger("DoSlam");
            }
        }
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


    void SlamAttack(Collider[] enemiesInRange)
    {
        Debug.Log("Golem Slam Activated!");

        foreach (Collider col in enemiesInRange)
        {
            Enemy enemy = col.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(slamDamage);
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
            if (distanceToEnemy < shortestDistance && distanceToEnemy <= slamRange)
            {
                shortestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, slamRange);
    }
}

