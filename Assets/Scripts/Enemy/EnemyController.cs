using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private EnemyState currentState;
    public Transform[] targetPoints;
    [SerializeField] private Transform targetPointsParent;

    [Range(0f, 0.5f)]
    public float accuracy = 0.1f;

    [Range(0f, 5f)]
    public float waitTime = 1f;

    public bool isRandom = false;

    public Transform enemyEye;

    [Range(0, 2.0f)]
    public float checkRadius = 0.4f;
    public float playerDistance;
    [Range(2.2f, 15f)]
    public float followRange = 10f;

    [Range(0.2f, 2f)]
    public float attackRange = 2f;

    [HideInInspector] public Transform player;

    public NavMeshAgent agent;

    public Color debugColor = Color.yellow;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GetTargetPositions();
        currentState = new EnemyIdleState(this);
        currentState.OnStateEnter();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnStateUpdate();
    }

    public void ChangeState(EnemyState state)
    {
        currentState.OnStateExit();
        currentState = state;
        currentState.OnStateEnter();
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

    private void OnDrawGizmos()
    {
        Gizmos.color = debugColor;



        Gizmos.DrawWireSphere(enemyEye.position, checkRadius);
        Gizmos.DrawWireSphere(enemyEye.position + enemyEye.forward * playerDistance, checkRadius);
        Gizmos.DrawLine(enemyEye.position, enemyEye.position + enemyEye.forward * playerDistance);
    }
}
