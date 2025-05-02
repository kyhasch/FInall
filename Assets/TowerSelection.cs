using UnityEngine;

public class TowerSelection : MonoBehaviour
{
    public static TowerSelection instance;
    public GameObject selectedTower;

    void Awake()
    {
        instance = this;
    }

    public void SelectTower(GameObject tower)
    {
        selectedTower = tower;
        UIManager.instance.ShowUpgradeSellUI(true);
    }

    public void DeselectTower()
    {
        selectedTower = null;
        UIManager.instance.ShowUpgradeSellUI(false);
    }
}
