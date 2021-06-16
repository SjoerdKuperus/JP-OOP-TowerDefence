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
    protected List<EnemyUnit> enemiesInRange;
    private bool inCooldown;
    private float coolDownTime = 0f;    

    public virtual TowerType TowerType
    {
        get
        {
            return TowerType.BasicTower;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        var rangeIndicatorMeshRenderer = RangeIndicator.GetComponent<MeshRenderer>();
        rangeIndicatorMeshRenderer.enabled = false;
        enemiesInRange = new List<EnemyUnit>();
        inCooldown = false;
        if(SmokeShotParticle != null)
        {
            SmokeShotParticle.Stop();
        }        
    }

    public virtual int GetCost()
    {
        return 25;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesInRange.Count > 0 && !inCooldown)
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
        if(enemiesInRange.Count == 0)
        {
            //No enemies to shoot;
            return;
        }

        // Get the first enemy, and hit it
        var firstEnemy = enemiesInRange.First();
        // Check if not already detroyed by other tower
        if(firstEnemy == null)
        {
            enemiesInRange.Remove(firstEnemy);
            Shoot(); //Try again on next target, if any are avaible.
        }
        else
        {
            SmokeShotParticle.transform.LookAt(firstEnemy.transform);
            SmokeShotParticle.Play();
            firstEnemy.Hit(5);
            if (firstEnemy.IsDestroyed())
            {
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
    BasicTower = 0,
    CannonTower = 1,
    SpeedTower = 2,
    PosionTower = 3,
    FreezeTower = 4,
    LightningTower = 5   
}
