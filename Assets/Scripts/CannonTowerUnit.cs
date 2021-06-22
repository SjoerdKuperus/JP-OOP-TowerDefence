using System.Linq;
using UnityEngine;

public class CannonTowerUnit : TowerUnit
{
    public GameObject CannonballPrefab;
    private GameObject ProjectilesParrent;

    protected override void Awake()
    {
        ProjectilesParrent = GameObject.Find("Projectiles");
        base.Awake();
    }

    public override TowerType TowerType
    {
        get
        {
            return TowerType.CannonTower;
        }
    }

    public override int GetCost()
    {
        return 50;
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
       
        //Launch projectile towards target.
        var towerTop = new Vector3(transform.position.x, 5, transform.position.z);
        var ball = Instantiate(CannonballPrefab, towerTop, Quaternion.identity, ProjectilesParrent.transform);
        Vector3 shootVector = GetBallisticVector(firstEnemy.transform.position, ball.transform.position, 45);

        ball.GetComponent<Rigidbody>().AddForce(shootVector, ForceMode.Impulse);
    }

    private Vector3 GetBallisticVector(Vector3 targetPosition, Vector3 ownPosition, float angle)
    {
        var dir = targetPosition - ownPosition;  // get target direction
        var h = dir.y;  // get height difference 
        dir.y = 0;  // retain only the horizontal direction
        var dist = dir.magnitude;  // get horizontal distance
        var a = angle * Mathf.Deg2Rad;  // convert angle to radians
        dir.y = dist * Mathf.Tan(a);  // set dir to the elevation angle
        dist += h / Mathf.Tan(a);  // correct for small height differences
        // calculate the velocity magnitude
        var vel = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        return vel * dir.normalized;
    }
}
