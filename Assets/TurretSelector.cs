using UnityEngine;

public class TurretSelector : MonoBehaviour
{
    public static TurretSelector instance;

    [Header("Turret Prefabs")]
    public GameObject slimeTurretPrefab;
    public GameObject spiderTurretPrefab;
    public GameObject golemTurretPrefab;

    [HideInInspector]
    public GameObject selectedTurretPrefab;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void SelectSlimeTurret()
    {
        selectedTurretPrefab = slimeTurretPrefab;
        Debug.Log("Selected: Slime Turret");
    }

    public void SelectSpiderTurret()
    {
        selectedTurretPrefab = spiderTurretPrefab;
        Debug.Log("Selected: Spider Turret");
    }

    public void SelectGolemTurret()
    {
        selectedTurretPrefab = golemTurretPrefab;
        Debug.Log("Selected: Golem Turret");
    }
}
