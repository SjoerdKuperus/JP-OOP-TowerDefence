public class FreezeTowerUnit : TowerUnit
{
    public override TowerType TowerType
    {
        get
        {
            return TowerType.FreezeTower;
        }
    }

    public override int GetCost()
    {
        return 150;
    }

    internal override void Shoot()
    {
        foreach(var enemy in targetableEnemies)
        {
            if(enemy != null)
            {
                enemy.AddFreezeEffect(2f);
            }
        }
    }
}
