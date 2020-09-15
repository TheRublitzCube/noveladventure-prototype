using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Runtime.InteropServices.WindowsRuntime;

public class EnemyBaseChase : MonoBehaviour
{
    public Transform target;

    public float speed = 200f; //speed of movement
    public float nextWaypointDistance = 3f; //How close enemy is waypoint before moving on to the next one

    Path path; //current path being followed
    int currentWaypoint = 0; //current waypoint on path
    bool reachedEndOfPath = false; //checks to reach end of path

    Seeker seeker; //seeker script
    Rigidbody2D rb;

    EnemyBaseClass enemy;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
        seeker.StartPath(rb.position, target.position, OnPathComplete);

        enemy = GetComponent<EnemyBaseClass>();

        
    }


    private void FixedUpdate()
    {
        if (!enemy.knockedOut)
        {
            Chase();
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void Chase()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    private void UpdatePath()
    {
        if (seeker.IsDone() && !enemy.knockedOut)
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }

        if (enemy.knockedOut)
        {
            CancelInvoke();
            rb.velocity = Vector2.zero;
        }
    }
}
