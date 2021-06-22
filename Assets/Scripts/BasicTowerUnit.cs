using System.Linq;
using UnityEngine;

public class BasicTowerUnit : TowerUnit
{
    [SerializeField]
    private ParticleSystem SmokeShotParticle;

    public override TowerType TowerType
    {
        get
        {
            return TowerType.BasicTower;
        }
    }
    protected override void Awake()
    {
        if (SmokeShotParticle != null)
        {
            SmokeShotParticle.Stop();
        }
        base.Awake();
    }

    public override int GetCost()
    {
        return 25;
    }

    internal override void Shoot()
    {
        if (targetableEnemies.Count == 0)
        {
            //No enemies to shoot;
            return;
        }

        // Get the first enemy, and hit it
        var firstEnemy = targetableEnemies.First();

        SmokeShotParticle.transform.LookAt(firstEnemy.transform);
        SmokeShotParticle.Play();
        audioData.Play();
        firstEnemy.Hit(5);
    }
}
