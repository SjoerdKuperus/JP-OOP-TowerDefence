using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    private GameObject spawn;
    private GameObject goal;
    private List<GameObject> checkPoints;
    [SerializeField]
    private GameObject healthbar;
    public float speed;
    public float maxHitPoints;
    private float hitPoints;
    private bool isPoisoned;
    private bool isSlowed;
    private float hitPointLossPerSecond;
    public AudioClip audioClip;
    public ParticleSystem poisonParticleSystem;
    public float slowDurationTime = 0f;

    void Awake()
    {
        spawn = GameObject.Find("StartSpawn");
        goal = GameObject.Find("EndGoal");
        checkPoints = MainManager.Instance.LevelPathManager.GetCheckPoints();
        poisonParticleSystem.Stop();
        hitPoints = maxHitPoints;
        UpdateHealtBar();
    }

    private void UpdateHealtBar()
    {
        var healthBarRenderer = healthbar.GetComponent<Renderer>();
        if(maxHitPoints == hitPoints)
        {
            healthBarRenderer.material.color = Color.green;
        }
        else if ((maxHitPoints / 2) < hitPoints)
        {
            healthBarRenderer.material.color = Color.yellow;
        }
        else
        {
            healthBarRenderer.material.color = Color.red;
        }
        healthbar.transform.localScale = new Vector3((hitPoints / maxHitPoints), 0.1f, 0.2f);
        healthbar.transform.localPosition = new Vector3((1 - (hitPoints / maxHitPoints)) / 2, 0.45f, 0.6f);
    }

    // Update is called once per frame
    void Update()
    {
        // Enemy moves
        Move();

        CheckSlowDuration();

        // Check distance to goal and handle
        if (Vector3.Distance(transform.position, goal.transform.position) < 0.001f)
        {
            ReachedGoal();
        }
        if (checkPoints.Any())
        {
            if (Vector3.Distance(transform.position, checkPoints.First().transform.position) < 0.001f)
            {
                //Checkpoint reached, remove and navigate to next or end goal.
                checkPoints.Remove(checkPoints.First());
            }
        }            
    }    

    // Check if we got hit by towers. Then destroy.
    private void LateUpdate()
    {
        if(isPoisoned)
        {
            hitPoints -= hitPointLossPerSecond * Time.deltaTime;
        }

        UpdateHealtBar();

        if (hitPoints <= 0)
        {
            MainManager.Instance.IncreaseScore(5);
            MainManager.Instance.StartDestroyAnimation(transform.position);
            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position, 0.4f);
            Destroy(gameObject);
        }
    }

    internal bool IsTargetable()
    {
        return transform.position.x > -34;
    }

    internal virtual void ReachedGoal()
    {
        MainManager.Instance.ReduceLives();
        Destroy(gameObject);
    }

    internal void AddPoisonEffect(float _hitPointLossPerSecond)
    {
        isPoisoned = true;
        hitPointLossPerSecond = _hitPointLossPerSecond;
        poisonParticleSystem.Play();
    }

    internal void AddFreezeEffect(float freezeDuration)
    {
        isSlowed = true;
        slowDurationTime = freezeDuration;
    }

    private void CheckSlowDuration()
    {
        if(slowDurationTime > 0)
        {
            slowDurationTime -= Time.deltaTime;
        }
        else
        {
            slowDurationTime = 0;
            isSlowed = false;
        }
    }

    internal virtual void Move()
    {
        // Move from the current position towards the goal.
        float step = speed * Time.deltaTime * (isSlowed ? 0.5f: 1f);

        //If there are any checkpoints to go towards, move to those, else move towards endgoal.
        if (checkPoints.Any())
        {
            transform.position = Vector3.MoveTowards(transform.position, checkPoints.First().transform.position, step);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, goal.transform.position, step);
        }        
    }

    internal void Hit(int damage)
    {
        //Check position of the enemy. Should be invurable until passed the gate.
        if (IsTargetable())
        {
            hitPoints -= damage;
        }
    }

    internal bool IsDestroyed()
    {
        return hitPoints <= 0;
    }
}
