using System;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    private GameObject spawn;
    private GameObject goal;
    public float speed;

    void Awake()
    {
        spawn = GameObject.Find("StartSpawn");
        goal = GameObject.Find("EndGoal");
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

    internal virtual void ReachedGoal()
    {
        MainManager.Instance.ReduceLives();
        Destroy(gameObject);
    }

    internal virtual void Move()
    {
        // Move from the current position towards the goal.
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, goal.transform.position, step);
    }

    internal bool Hit()
    {
        //Check position of the enemy. Should be invurable until passed the gate.
        if(transform.position.x < -35)
        {
            return false;
        }

        MainManager.Instance.IncreaseScore(5);
        MainManager.Instance.StartDestroyAnimation(transform.position);
        Destroy(gameObject);
        return true;
    }
}
