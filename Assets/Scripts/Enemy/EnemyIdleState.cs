using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    private int currentTarget = 0;

    private float timer;
    public EnemyIdleState(EnemyController enemy): base(enemy)
    {

    }
    public override void OnStateEnter()
    {
        enemy.agent.destination = enemy.targetPoints[currentTarget].position;
        timer = enemy.waitTime;
        enemy.debugColor = Color.yellow;
        Debug.Log("Enemy entered Idle");
    }

    public override void OnStateExit()
    {
        Debug.Log("Enemy is no more Idle");
    }

    public override void OnStateUpdate()
    {
        Debug.Log("Entered Idle Update");
        PatrolAtPosition();
        if (Physics.SphereCast(enemy.enemyEye.position, enemy.checkRadius, enemy.transform.forward, out RaycastHit hit, enemy.playerDistance))
        {
            if (hit.transform.CompareTag("Player"))
            {
                Debug.Log("Found Player");

                enemy.player = hit.transform;
                enemy.agent.destination = enemy.player.position;

                enemy.ChangeState(new EnemyFollowState(enemy));
            }
        }
    }
    private void PatrolAtPosition()
    {
        if (enemy.agent.remainingDistance < enemy.accuracy) //Checking distance between enemy and target position
        {
            SetNewTargetPosition();
            timer -= Time.deltaTime;

            if (timer <= 0)
            {

                //Set new enemy position
                enemy.agent.destination = enemy.targetPoints[currentTarget].position;
                timer = enemy.waitTime;
            }
        }
       
    }

    private void SetNewTargetPosition()
    {
        if (enemy.isRandom)
        {
            Debug.Log("Randomising positions");
            int randomInt = -1;
            do
            {
                randomInt = Random.Range(0, enemy.targetPoints.Length);
            } while (randomInt == currentTarget);

            currentTarget = randomInt;
        }
        else
        {
            currentTarget++;
            if (currentTarget >= enemy.targetPoints.Length) //Ensuring the current Target does not exceed the number of target points
            {
                currentTarget = 0;
            }
        }


    }
}
