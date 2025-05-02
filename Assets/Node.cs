using UnityEngine;

public class Node : MonoBehaviour
{
    public bool isOccupied = false;  // To check if a turret is already placed
    public GameObject turret = null; // Reference to the turret instance

    private Renderer rend;  // To change node visuals (e.g., when occupied)
    private Color startColor; // Original color of the node

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;  // Store original color for reset
    }

    void OnMouseDown()
    {
        // 🟨 Handle Sell Mode
        if (UIManager.instance != null && UIManager.instance.isSellMode)
        {
            if (isOccupied && turret != null)
            {
                Destroy(turret);
                turret = null;
                isOccupied = false;
                ChangeNodeColor(startColor);

                PlayerStats.instance.EarnMoney(50);  // Flat refund (or logic based on upgrade state)

                UIManager.instance.isSellMode = false;
                Debug.Log("Tower sold. Sell mode exited.");
            }
            else
            {
                Debug.Log("No turret to sell on this node.");
                UIManager.instance.isSellMode = false; // Optional: still exit sell mode if nothing to sell
            }

            return; // Skip placement logic if selling
        }

        // 🟩 Place turret if not in sell mode
    if (!isOccupied)
{
    GameObject turretToBuild = TurretSelector.instance.selectedTurretPrefab;
    if (turretToBuild == null)
    {
        Debug.LogWarning("No turret selected from UI.");
        return;
    }

    int turretCost = 100;
    if (PlayerStats.instance.money < turretCost)
    {
        Debug.Log("Not enough money to build turret.");
        return;
    }

    PlayerStats.instance.SpendMoney(turretCost);
    turret = Instantiate(turretToBuild, transform.position, Quaternion.identity);
    isOccupied = true;

    ChangeNodeColor(Color.red);
}

    }

    public void ResetNode()
    {
        if (turret != null)
        {
            Destroy(turret);
            turret = null;
            isOccupied = false;
            ChangeNodeColor(startColor);
        }
    }

    private void ChangeNodeColor(Color color)
    {
        rend.material.color = color;
    }
}
