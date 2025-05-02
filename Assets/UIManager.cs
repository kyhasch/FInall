using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Button sellButton;

    [HideInInspector]
    public bool isSellMode = false;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void ShowUpgradeSellUI(bool show)
    {
        // Optional if you still want to hide/show UI
        sellButton.gameObject.SetActive(show);
    }

    public void OnSellPressed()
    {
        isSellMode = true;
        Debug.Log("Sell mode activated — click a node to remove its tower.");
    }
}
