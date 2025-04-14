using UnityEngine;

public class TurretPlacement : MonoBehaviour
{
    public GameObject turretPrefab; // The turret prefab to place
    public Camera playerCamera;
    public LayerMask nodeLayer;
    public int turretCost = 100; // Cost per turret

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // Left mouse click to place turret
        {
            if (PlayerStats.instance.money >= turretCost)
            {
                Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, nodeLayer))
                {
                    Node node = hit.transform.GetComponent<Node>();
                    if (node != null && !node.isOccupied) // Check if the node is free
                    {
                        node.PlaceTurret();  // Place the turret
                        PlayerStats.instance.SpendMoney(turretCost);  // Centralized logic
                    }
                }
            }
        }
    }
}
