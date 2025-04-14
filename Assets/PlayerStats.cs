using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public int money = 200;
    public Text moneyText;

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
