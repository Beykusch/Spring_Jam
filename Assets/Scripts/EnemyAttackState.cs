using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackState : StateMachineBehaviour
{
    Transform player;
    NavMeshAgent agent;

    float stopAttackingDistance = 15f;
    Collider damageCollider;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
        float sqrStopAttackingDistance = 15f * 15f;
        float distanceSqr = (player.position - animator.transform.position).sqrMagnitude;
        if (distanceSqr > sqrStopAttackingDistance)
            animator.SetBool("isAttacking", false);

        damageCollider = animator.transform.Find("EnemyBullet").GetComponent<Collider>();
        if (damageCollider != null)
            damageCollider.enabled = false;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        LookAtPlayer();

        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);

        if (distanceFromPlayer > stopAttackingDistance)
        {
            animator.SetBool("isAttacking", false);
        }

    }

    private void LookAtPlayer()
    {
        Vector3 direction = (player.position - agent.transform.position).normalized;
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            agent.transform.rotation = Quaternion.RotateTowards(agent.transform.rotation, lookRotation, Time.deltaTime * 200f);
        }
    }

}
