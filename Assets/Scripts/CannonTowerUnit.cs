using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CannonTowerUnit : TowerUnit
{
    public override TowerType TowerType
    {
        get
        {
            return TowerType.CannonTower;
        }
    }

    internal override void Shoot()
    {
        if (enemiesInRange.Count == 0)
        {
            //No enemies to shoot;
            return;
        }
        //Launce projectile towards target.
        Debug.Log("Cannon tower shoots!");
    }
}
