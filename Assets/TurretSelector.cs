using UnityEngine;

public class TurretSelector : MonoBehaviour
{
    public static TurretSelector instance;

    public GameObject selectedTurretPrefab;

    void Awake()
    {
        instance = this;
    }

    public void SelectTurret(GameObject turret)
    {
        selectedTurretPrefab = turret;
    }
}
