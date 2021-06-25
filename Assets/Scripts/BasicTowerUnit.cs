using System;
using System.Linq;
using UnityEngine;

public class BasicTowerUnit : TowerUnit
{
    [SerializeField]
    private ParticleSystem SmokeShotParticle;

    [SerializeField]
    private GameObject SpotLight;
    public float trackLightSpeed = 0.001f;

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
        if(SpotLight != null)
        {
            var light = SpotLight.GetComponentInChildren<Light>();
            light.enabled = false;
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

    public override void Update()
    {
        base.Update();
        TrackEnemyWithLight();
    }

    private void TrackEnemyWithLight()
    {
        var light = SpotLight.GetComponentInChildren<Light>();
        if (targetableEnemies.Count > 0)
        {            
            light.enabled = true;
            var firstEnemy = targetableEnemies.First();
            
            // the vector of the position of the enemy
            Vector3 direction = firstEnemy.transform.position - SpotLight.transform.position;

            // A Quaternion with the rotation to look at this enemy (facing our own forward vector to it)
            Quaternion toRotation = Quaternion.FromToRotation(transform.forward, direction);

            // Do an Interpolation between the current rotation and the desired direction.
            // This smooths out the rotation over time.
            SpotLight.transform.rotation = Quaternion.Lerp(SpotLight.transform.rotation, toRotation, trackLightSpeed * Time.time);

        }
        else
        {
            light.enabled = false;
        }
    }
}
