using UnityEngine;

public class Slime_Tower : MonoBehaviour, IUpgradeable
{
    public GameObject projectilePrefab;
    public float range = 10f;
    public float fireRate = 1f;
    public float damage = 10f;

    public bool isUpgraded = false;
    public int baseCost = 100;

    private float fireCountdown = 0f;

    void Update()
    {
        RotateTowardEnemy();

        fireCountdown -= Time.deltaTime;
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
    }

    void Shoot()
    {
        GameObject target = FindClosestEnemy();
        if (target != null)
        {
            GameObject projectileGO = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Projectile proj = projectileGO.GetComponent<Projectile>();
            if (proj != null)
            {
                proj.Seek(target.transform);
                proj.damage = damage;
            }
        }
    }

    GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float shortest = Mathf.Infinity;

        foreach (GameObject e in enemies)
        {
            float dist = Vector3.Distance(transform.position, e.transform.position);
            if (dist < shortest && dist <= range)
            {
                shortest = dist;
                closest = e;
            }
        }
        return closest;
    }

    void RotateTowardEnemy()
    {
        GameObject target = FindClosestEnemy();
        if (target != null)
        {
            Vector3 dir = target.transform.position - transform.position;
            dir.y = 0f;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    public void Upgrade()
    {
        if (isUpgraded) return;

        range *= 2f;
        damage *= 2f;
        isUpgraded = true;

        Debug.Log("Slime Tower upgraded!");
    }

    public int GetSellValue()
    {
        return isUpgraded ? 150 : 50;
    }

    public bool IsUpgraded => isUpgraded;

    void OnMouseDown()
    {
         Debug.Log("Tower clicked!");
        TowerSelection.instance.SelectTower(this.gameObject);
    }
}
