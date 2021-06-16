using System;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    private GameObject spawn;
    private GameObject goal;
    public float speed;
    private float hitPoints;
    private bool isPoisoned;
    private float hitPointLossPerSecond;

    void Awake()
    {
        spawn = GameObject.Find("StartSpawn");
        goal = GameObject.Find("EndGoal");
        hitPoints = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Enemy moves
        Move();

        // Check distance to goal and handle
        if (Vector3.Distance(transform.position, goal.transform.position) < 0.001f)
        {
            ReachedGoal();
        }
    }

    // Check if we got hit by towers. Then destroy.
    private void LateUpdate()
    {
        if(isPoisoned)
        {
            hitPoints -= hitPointLossPerSecond * Time.deltaTime;
        }    

        if (hitPoints <= 0)
        {
            MainManager.Instance.IncreaseScore(5);
            MainManager.Instance.StartDestroyAnimation(transform.position);
            Destroy(gameObject);
        }
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
    }

    internal virtual void Move()
    {
        // Move from the current position towards the goal.
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, goal.transform.position, step);
    }

    internal void Hit(int damage)
    {
        //Check position of the enemy. Should be invurable until passed the gate.
        if (transform.position.x > -35)
        {
            hitPoints = -damage;
        }
    }

    internal bool IsDestroyed()
    {
        return hitPoints <= 0;
    }
}
