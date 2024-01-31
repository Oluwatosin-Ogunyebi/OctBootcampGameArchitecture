using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowState : EnemyState
{
    public EnemyFollowState(EnemyController enemy) : base(enemy)
    {

    }
    public override void OnStateEnter()
    {   
        enemy.debugColor = Color.green;
        Debug.Log("Enemy entered Follow State");
    }

    public override void OnStateExit()
    {
        Debug.Log("Enemy stopped following Player");
    }

    public override void OnStateUpdate()
    {
        if (enemy.player != null)
        {
            if (Vector3.Distance(enemy.transform.position, enemy.player.position) > enemy.followRange)
            {
                //Go back to Idle
                enemy.ChangeState(new EnemyIdleState(enemy));
            }

            //Attack Player
            if (Vector3.Distance(enemy.transform.position, enemy.player.position) < enemy.attackRange)
            {
                //Go to Attack
                enemy.ChangeState(new EnemyAttackState(enemy));
            }

            enemy.agent.destination = enemy.player.position;
        }
        else
        {
            enemy.ChangeState(new EnemyIdleState(enemy));
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
