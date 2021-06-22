using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerUnit : MonoBehaviour
{
    [SerializeField]
    private GameObject RangeIndicator;
    [SerializeField]
    private ParticleSystem SmokeShotParticle;
    [SerializeField]
    private float shootCooldown;

    private bool towerIsSelected;
    private List<EnemyUnit> enemiesInRange;
    protected List<EnemyUnit> targetableEnemies;
    private bool inCooldown;
    private float coolDownTime = 0f;
    
    protected AudioSource audioData;

    public virtual TowerType TowerType
    {
        get
        {
            return TowerType.BasicTower;
        }
    }
    protected virtual void Awake()
    {
        var rangeIndicatorMeshRenderer = RangeIndicator.GetComponent<MeshRenderer>();
        rangeIndicatorMeshRenderer.enabled = false;
        enemiesInRange = new List<EnemyUnit>();
        inCooldown = false;
        if (SmokeShotParticle != null)
        {
            SmokeShotParticle.Stop();
        }
        audioData = GetComponent<AudioSource>();
    }

    public virtual int GetCost()
    {
        return 25;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTargetableEnemies();

        if (targetableEnemies.Count > 0 && !inCooldown)
        {
            Shoot();
            inCooldown = true;
            coolDownTime = shootCooldown;

        }
        if (inCooldown)
        {
            coolDownTime -= Time.deltaTime;
            if (coolDownTime <= 0)
            {
                inCooldown = false;
            }
        }
    }

    private void UpdateTargetableEnemies()
    {
        targetableEnemies = new List<EnemyUnit>();
        foreach (var enemy in enemiesInRange)
        {
            if (enemy != null && enemy.IsTargetable())
            {
                targetableEnemies.Add(enemy);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            EnemyUnit enemyUnit = other.gameObject.GetComponent<EnemyUnit>();
            if (!enemiesInRange.Contains(enemyUnit))
            {
                enemiesInRange.Add(enemyUnit);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            EnemyUnit enemyUnit = other.gameObject.GetComponent<EnemyUnit>();
            if (enemiesInRange.Contains(enemyUnit))
            {
                enemiesInRange.Remove(enemyUnit);
            }
        }
    }


    internal virtual void Shoot()
    {
        if(targetableEnemies.Count == 0)
        {
            //No enemies to shoot;
            return;
        }

        // Get the first enemy, and hit it
        var firstEnemy = targetableEnemies.First();
        // Check if not already detroyed by other tower
        if(firstEnemy == null)
        {
            targetableEnemies.Remove(firstEnemy);
            enemiesInRange.Remove(firstEnemy);
            Shoot(); //Try again on next target, if any are avaible.
        }
        else
        {
            SmokeShotParticle.transform.LookAt(firstEnemy.transform);
            SmokeShotParticle.Play();
            audioData.Play();
            firstEnemy.Hit(5);
            if (firstEnemy.IsDestroyed())
            {
                targetableEnemies.Remove(firstEnemy);
                enemiesInRange.Remove(firstEnemy);
            }
        }        
    }

    public void SelectTower()
    {
        var rangeIndicatorMeshRenderer = RangeIndicator.GetComponent<MeshRenderer>();
        rangeIndicatorMeshRenderer.enabled = true;
    }

    public void DeselectTower()
    {
        var rangeIndicatorMeshRenderer = RangeIndicator.GetComponent<MeshRenderer>();
        rangeIndicatorMeshRenderer.enabled = false;
    }
}

public enum TowerType
{
    None = 0,
    BasicTower = 1,
    CannonTower = 2,
    SpeedTower = 3,
    PoisonTower = 4,
    FreezeTower = 5,
    LightningTower = 6
}
