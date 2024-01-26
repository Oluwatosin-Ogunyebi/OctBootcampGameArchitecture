using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] targetPoints;
    [SerializeField] private Transform targetPointsParent;

    [Range(0f, 0.5f)]
    [SerializeField] private float accuracy = 0.1f;

    [Range(0f, 5f)]
    [SerializeField] private float waitTime = 1f;

    [SerializeField] private bool isRandom = false;

    [SerializeField] private Transform enemyEye;

    [Range(0,2.0f)]
    [SerializeField] private float checkRadius = 0.4f;
    [SerializeField] private float playerDistance;

    [Range(2.2f,15f)]
    [SerializeField] private float followRange = 10f;

    [Range(0.2f, 2f)]
    [SerializeField] private float attackRange = 2f;

    public Transform player;

    private int currentTarget = 0;
    private NavMeshAgent agent;

    private bool isIdle = true;
    private bool isPlayerFound;
    private bool isCloseToPlayer;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GetTargetPositions();
        agent.destination = targetPoints[currentTarget].position;




    }

    private void Idle() //Idle state of enemy
    {
        StartCoroutine(PatrolAtPosition(waitTime));
        if (Physics.SphereCast(enemyEye.position, checkRadius, transform.forward, out RaycastHit hit, playerDistance))
        {
            if (hit.transform.CompareTag("Player"))
            {
                Debug.Log("Found Player");
                isIdle = false;
                isPlayerFound = true;

                player = hit.transform;
                agent.destination = player.position;
            }
        }
    }

    private void FollowPlayer()
    {
        if (player != null)
        {
            if (Vector3.Distance(transform.position, player.position) > followRange)
            {
                isPlayerFound = false;
                isIdle = true;
            }

            //Attack Player
            if (Vector3.Distance(transform.position, player.position) < attackRange)
            {
                isCloseToPlayer = true;
            }
            else
            {
                isCloseToPlayer = false;
            }

            agent.destination = player.position;
        }
        else
        {
            isPlayerFound = false;
            isIdle = true;
            isCloseToPlayer = false;
        }
    }

    private void AttackPlayer()
    {
        Debug.Log("Attacking Players");
        if(Vector3.Distance(transform.position, player.position) > attackRange)
        {
            isCloseToPlayer = false;
        }
    }

    public void GetTargetPositions()
    {   
        targetPoints = new Transform[targetPointsParent.childCount];
        Debug.Log(targetPointsParent.childCount);
        for (int i = 0; i < targetPointsParent.childCount; i++)
        {
            targetPoints[i] = targetPointsParent.GetChild(i);
        }
    }

    void Update()
    {
        if (isIdle)
        {
            Idle();
        }
        else if (isPlayerFound)
        {
            if (isCloseToPlayer)
            {
                AttackPlayer();
            }
            else
            {
                FollowPlayer();
            }
        }
    }

    private void SetNewTargetPosition()
    {   
        if (isRandom)
        {
            Debug.Log("Randomising positions");
            int randomInt = -1;
            do
            {
                randomInt = Random.Range(0, targetPoints.Length);
            } while (randomInt == currentTarget);

            currentTarget = randomInt;
        }
        else
        {
            currentTarget++;
            if (currentTarget >= targetPoints.Length) //Ensuring the current Target does not exceed the number of target points
            {
                currentTarget = 0;
            }
        }

            
    }

    private IEnumerator PatrolAtPosition(float waitTime)
    {
            Debug.Log("Coroutine Idle");
            yield return new WaitForSeconds(waitTime);
            if (agent.remainingDistance < accuracy) //Checking distance between enemy and target position
            {
                SetNewTargetPosition();
            }
            agent.destination = targetPoints[currentTarget].position; //Set new enemy position

        
    }

    private void OnDrawGizmos()
    {   
        if (isIdle)
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.green;
        }
        

        Gizmos.DrawWireSphere(enemyEye.position, checkRadius);
        Gizmos.DrawWireSphere(enemyEye.position + enemyEye.forward * playerDistance, checkRadius);
        Gizmos.DrawLine(enemyEye.position, enemyEye.position + enemyEye.forward * playerDistance);
    }
}
