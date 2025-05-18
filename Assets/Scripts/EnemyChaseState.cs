using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : StateMachineBehaviour
{
    Transform player;
    NavMeshAgent agent;

    public float chaseSpeed = 5f;

    public float stopChasingDistance = 25f;
    public float attackingDistance = 18f;



    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();

        agent.speed = chaseSpeed;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(player.position);
        animator.transform.LookAt(player);

        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);

        if (distanceFromPlayer > stopChasingDistance)
        {
            animator.SetBool("isChasing", false);
            animator.SetBool("isAttacking", false); // guarantie
        }

        if (distanceFromPlayer < attackingDistance && !animator.GetBool("isAttacking"))
        {
            animator.SetBool("isAttacking", true);
            animator.SetBool("isChasing", false); // no attack and chase at the same time
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
    }
}
