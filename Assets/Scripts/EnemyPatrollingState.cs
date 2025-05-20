using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrollingState : StateMachineBehaviour
{
    private Transform player;
    private NavMeshAgent agent;
    private Enemy enemyData;
    private float timer;

    public float patrolSpeed = 2f;
    public float patrollingTime = 6f;
    public float detectionArea = 10f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
        enemyData = animator.GetComponent<Enemy>();

        agent.speed = patrolSpeed;
        timer = 0;

        // Entry waypoints
        if (enemyData.waypointsList.Count == 0)
        {
            foreach (Transform t in enemyData.waypointParent)
            {
                enemyData.waypointsList.Add(t);
            }
        }

        if (enemyData.waypointsList.Count > 0)
        {
            Vector3 nextPosition = enemyData.waypointsList[Random.Range(0, enemyData.waypointsList.Count)].position;
            agent.SetDestination(nextPosition);
        }
        else
        {
            Debug.LogError("Waypoint list is empty in " + animator.gameObject.name);
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent.remainingDistance <= agent.stoppingDistance && enemyData.waypointsList.Count > 0)
        {
            Vector3 nextPosition = enemyData.waypointsList[Random.Range(0, enemyData.waypointsList.Count)].position;
            agent.SetDestination(nextPosition);
        }

        timer += Time.deltaTime;
        if (timer > patrollingTime)
        {
            animator.SetBool("isPatrolling", false);
        }

        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);
        if (distanceFromPlayer < detectionArea)
        {
            animator.SetBool("isChasing", true);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
    }
}
