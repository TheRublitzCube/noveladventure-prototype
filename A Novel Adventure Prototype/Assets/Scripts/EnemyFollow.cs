using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyFollow : MonoBehaviour
{
    public Transform target;

    public float chaseSpeed; //speed of movement when chasing
    public float wanderSpeed; 
    public float nextWaypointDistance = 3f; //How close enemy is waypoint before moving on to the next one

    public Transform[] wanderPoints;
    int currentWanderPointIndex;
    public float newTargetDistance;

    Path path; //current path being followed
    int currentWaypoint = 0; //current waypoint on path we are targeting
    bool reachedEndOfPath = false; //checks to reach end of path

    Seeker seeker; //seeker script
    Rigidbody2D rb; //rigidbody of enemy

    EnemyBaseClass enemy; //enemy script

    public enum EnemyState { Wander, Chase };
    public EnemyState enemyState;

    GameObject player;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        enemy = GetComponent<EnemyBaseClass>();
        enemyState = EnemyState.Wander;

        target = wanderPoints[Random.Range(0, wanderPoints.Length)];

        InvokeRepeating("UpdatePath", 0f, .5f);
        seeker.StartPath(rb.position, target.position, OnPathComplete);

        player = GameObject.FindGameObjectWithTag("Player");
    }


    private void FixedUpdate()
    {
        if (!enemy.knockedOut)
        {
            if (enemyState == EnemyState.Wander)
            {
                Wander();
            }

            else if (enemyState == EnemyState.Chase)
            {
                Chase();
            }
        }
    }

    //--------Function called when the path is complete--------//
    void OnPathComplete(Path p)
    {
        if (!p.error) //if we didn't get an error
        {
            path = p; //sets current path to new path
            currentWaypoint = 0; //resets progress on path
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
        Vector2 force = direction * chaseSpeed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    
    }

    void Wander()
    {

        if (path == null) //if there is no path
            return;

        if (currentWaypoint >= path.vectorPath.Count) //if the end of path is reached
        {
            reachedEndOfPath = true;
            return;
        }

        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * wanderSpeed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        float distanceToWanderPoint = Vector2.Distance(rb.position, target.position);

        if (distanceToWanderPoint < newTargetDistance)
        {
            int newCurrentPointIndex = Random.Range(0, wanderPoints.Length);

            if (newCurrentPointIndex == currentWanderPointIndex)
            {
                newCurrentPointIndex -= 1;

                if (newCurrentPointIndex == -1)
                {
                    newCurrentPointIndex = wanderPoints.Length - 1;
                }
            }

            currentWanderPointIndex = newCurrentPointIndex;

            target = wanderPoints[currentWanderPointIndex];
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

    public void SwitchEnemyState(EnemyState state)
    {
        if (state == EnemyState.Chase)
        {
            enemyState = EnemyState.Chase;
            target = player.transform;
            rb.drag = 1.5f;
        }

        else if (state == EnemyState.Wander)
        {
            enemyState = EnemyState.Wander;
            target = wanderPoints[currentWanderPointIndex];
            rb.drag = 1.0f;
        }
    }
    
}
