using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowAggroTrigger : MonoBehaviour
{

    EnemyFollow enemyAI; //enemy script
    EnemyBaseClass enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemyAI = GetComponentInParent<EnemyFollow>();
        enemy = GetComponentInParent<EnemyBaseClass>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !enemy.knockedOut)
        {
            enemyAI.SwitchEnemyState(EnemyFollow.EnemyState.Chase);
        }
    }
}
