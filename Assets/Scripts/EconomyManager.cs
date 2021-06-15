using UnityEngine;
using UnityEngine.UI;

public class EconomyManager : MonoBehaviour
{
    public Text moneyText;

    private int money;    

    internal void SetupGame()
    {
        money = 200;
        ShowMoney();
    }

    internal void AddMoney(int ammount)
    {
        money += ammount;
        ShowMoney();
    }

    internal bool ReduceMoney(int ammount)
    {
        if (money < ammount)
        {
            return false;
        }
        money -= ammount;
        ShowMoney();
        return true;
    }

    private void ShowMoney()
    {
        moneyText.text = money + "$";
    }
}
