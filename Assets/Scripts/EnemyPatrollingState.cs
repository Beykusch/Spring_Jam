using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrollingState : StateMachineBehaviour
{
    public string waypointParentName = "WaypointsEnemy1"; // change this part for spesific enemy

    private Transform player;
    private NavMeshAgent agent;
    private List<Transform> waypointsList = new List<Transform>();
    private float timer;
    public float patrolSpeed = 2f;
    public float patrollingTime = 6f;
    public float detectionArea = 10f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = patrolSpeed;
        timer = 0;
        waypointsList.Clear();

        // Find Waypoint object of each enemy
        Transform waypointCluster = animator.transform.Find(waypointParentName);
        if (waypointCluster != null)
        {
            foreach (Transform t in waypointCluster)
            {
                waypointsList.Add(t);
            }

            Vector3 nextPosition = waypointsList[Random.Range(0, waypointsList.Count)].position;
            agent.SetDestination(nextPosition);
        }
        else
        {
            Debug.LogWarning("Waypoint parent not found: " + waypointParentName);
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(waypointsList[Random.Range(0, waypointsList.Count)].position); 
        }

        timer += Time.deltaTime;
        if(timer > patrollingTime)
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
