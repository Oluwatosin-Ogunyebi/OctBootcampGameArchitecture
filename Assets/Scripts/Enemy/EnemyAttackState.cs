using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(EnemyController enemy): base(enemy)
    {

    }
    public override void OnStateEnter()
    {
        Debug.Log("Enemy in Attack State");
    }

    public override void OnStateExit()
    {
        Debug.Log("Enemy stopped attacking ");
    }
    public override void OnStateUpdate()
    {
        if (enemy.player != null)
        {

            //Attack Player
            if (Vector3.Distance(enemy.transform.position, enemy.player.position) > enemy.attackRange)
            {
                //Go to Follow
                enemy.ChangeState(new EnemyFollowState(enemy));
            }

            enemy.agent.destination = enemy.player.position;
        }
        else
        {
            //Go back to Idle
            enemy.ChangeState(new EnemyIdleState(enemy));
        }
    }

}
