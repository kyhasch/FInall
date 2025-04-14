using UnityEngine;

public class Node : MonoBehaviour
{
    public GameObject turretPrefab;  // The prefab of the turret to place
    public bool isOccupied = false;  // To check if a turret is already placed
    public GameObject turret = null; // Reference to the turret instance

    private Renderer rend;  // To change node visuals (e.g., when occupied)
    private Color startColor; // Original color of the node

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;  // Store original color for reset
    }

    // Place the turret on the node
    public void PlaceTurret()
    {
        if (isOccupied)
        {
            Debug.Log("Node already occupied.");
            return;
        }

        turret = Instantiate(turretPrefab, transform.position, Quaternion.identity);
        isOccupied = true;

        ChangeNodeColor(Color.red);  // Change color when occupied
    }

    // Reset the node (e.g., if turret is sold or destroyed)
    public void ResetNode()
    {
        if (turret != null)
        {
            Destroy(turret);  // Destroy the turret instance
            turret = null;  // Nullify the turret reference
            isOccupied = false;  // Mark the node as unoccupied
            ChangeNodeColor(startColor);  // Reset node color
        }
    }

    // Change the color of the node to indicate its state (occupied or not)
    private void ChangeNodeColor(Color color)
    {
        rend.material.color = color;  // Change the material color of the node
    }
}
