using System;
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

    internal bool BuildTower(TowerType placingTowerType)
    {
        int towerCost = 0;
        switch (placingTowerType)
        {
            case TowerType.CannonTower:
                towerCost = 50;
                break;
            case TowerType.BasicTower:
                towerCost = 25;
                break;
            case TowerType.FreezeTower:
            case TowerType.LightningTower:
            case TowerType.PosionTower:
            case TowerType.SpeedTower:
                Debug.Log("Tower type not yet implemented");
                break;
        }
        return ReduceMoney(towerCost);
    }
}
