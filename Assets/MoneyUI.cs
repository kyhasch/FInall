using UnityEngine;
using UnityEngine.UI;  // Use UnityEngine.UI for legacy Text

public class MoneyUI : MonoBehaviour
{
    public Text moneyText;  // Reference to the Text component

    void Update()
    {
        moneyText.text = "$" + PlayerStats.instance.money.ToString();
    }
}
