using UnityEngine;
using TMPro;  // Add this for TextMeshPro
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public int money = 300;
    public TextMeshProUGUI moneyText;  // Changed to TextMeshProUGUI

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        UpdateMoneyUI();
    }

    public void EarnMoney(int amount)
    {
        money += amount;
        UpdateMoneyUI();
    }

    public void SpendMoney(int amount)
    {
        money -= amount;
        UpdateMoneyUI();
    }

    void UpdateMoneyUI()
    {
        if (moneyText != null)
            moneyText.text = "Money: " + money.ToString();
        else
            Debug.LogWarning("MoneyText UI not assigned!");
    }
}
