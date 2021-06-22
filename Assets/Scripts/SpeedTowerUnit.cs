using System.Linq;
using UnityEngine;

public class SpeedTowerUnit : TowerUnit
{
    [SerializeField]
    private GameObject LazerGamePrefab;

    public override TowerType TowerType
    {
        get
        {
            return TowerType.SpeedTower;
        }
    }

    public override int GetCost()
    {
        return 75;
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
        if (firstEnemy == null)
        {
            targetableEnemies.Remove(firstEnemy);
            Shoot(); //Try again on next target, if any are avaible.
        }
        else
        {
            firstEnemy.Hit(2);
            var lazerClone = Instantiate(LazerGamePrefab, this.gameObject.transform);

            lazerClone.transform.LookAt(firstEnemy.transform);
            var lazerCloneParticleSystem = lazerClone.GetComponentInChildren<ParticleSystem>();
            var sh = lazerCloneParticleSystem.shape;

            float dist = Vector3.Distance(firstEnemy.transform.position, transform.position) - 2;
            sh.scale = new Vector3(1, 1, dist);
            sh.position = new Vector3(0, 0, (dist / 2) + 2);
            lazerCloneParticleSystem.Play();

            var lineRenderer = lazerClone.GetComponentInChildren<LineRenderer>();
            lineRenderer.SetPositions(new Vector3[] { new Vector3(transform.position.x, 3, transform.position.z), firstEnemy.transform.position });


            if (firstEnemy.IsDestroyed())
            {
                targetableEnemies.Remove(firstEnemy);
            }
        }
    }
}
