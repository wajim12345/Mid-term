using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowState : EnemyStates
{
    float distanceToPlayer;

    public EnemyFollowState(EnemyController _emeny) : base(_emeny)
    {

    }
    public override void OnStateEnter()
    {
        Debug.Log("Enemy will be following player");
    }

    public override void OnStateExit()
    {
        Debug.Log("Enemy will not follow player");
    }

    public override void OnStateUpdate()
    {
        if (enemy.player != null)
        {
            distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.position);
            if (distanceToPlayer > 10)
            {
                //Continue Patrolling
                enemy.ChangeState(new EnemyIdleState(enemy));
            }

            //Attack
            if (distanceToPlayer < 2)
            {
                enemy.ChangeState(new EnemyAttackState(enemy));
            }

            enemy.agent.destination = enemy.player.position;
        }
        else
        {
            enemy.ChangeState(new EnemyIdleState(enemy));
        }
    }

}
