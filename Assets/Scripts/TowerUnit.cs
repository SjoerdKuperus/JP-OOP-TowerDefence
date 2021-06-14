using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerUnit : MonoBehaviour
{
    [SerializeField]
    private GameObject RangeIndicator;

    private bool towerIsSelected;
    private List<EnemyUnit> enemiesInRange;
    private bool InCooldown;
    private float CoolDownTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        var rangeIndicatorMeshRenderer = RangeIndicator.GetComponent<MeshRenderer>();
        rangeIndicatorMeshRenderer.enabled = false;
        enemiesInRange = new List<EnemyUnit>();
        InCooldown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesInRange.Count > 0 && !InCooldown)
        {
            Shoot();
            InCooldown = true;
            CoolDownTime = 3.0f;

        }
        if (InCooldown)
        {
            CoolDownTime -= Time.deltaTime;
            if (CoolDownTime <= 0)
            {
                InCooldown = false;
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
            var isDestroyed = firstEnemy.Hit();
            if (isDestroyed)
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
