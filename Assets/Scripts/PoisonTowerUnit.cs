public class PoisonTowerUnit : TowerUnit
{
    public override TowerType TowerType
    {
        get
        {
            return TowerType.PoisonTower;
        }
    }

    public override int GetCost()
    {
        return 100;
    }

    internal override void Shoot()
    {
        foreach(var enemy in targetableEnemies)
        {
            if(enemy != null)
            {
                enemy.AddPoisonEffect(0.5f);
            }
        }
    }
}
