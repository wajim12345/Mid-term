using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyStates
{
    float distanceToPlayer;
    Health playerHealth;
    float damagePerSec = 20f;
    public EnemyAttackState(EnemyController _emeny) : base(_emeny)
    {
        playerHealth = _emeny.player.GetComponent<Health>();
    }
    public override void OnStateEnter()
    {
        Debug.Log("Enemy will attack the player");
    }

    public override void OnStateExit()
    {
        Debug.Log("Enemy will not attack the player");
    }

    public override void OnStateUpdate()
    {
        if (enemy.player != null)
        {
            distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.position);
            Debug.Log("Attacking Player!");
            if (distanceToPlayer > 2)
            {
                enemy.ChangeState(new EnemyFollowState(enemy));
            }

            enemy.agent.destination = enemy.player.position;
        }
        else
        {
            enemy.ChangeState(new EnemyIdleState(enemy));
        }
    }

    void Attack()
    {
        if(playerHealth != null)
        {
            playerHealth.DeductHealth(damagePerSec * Time.deltaTime);
        }
    }
}
