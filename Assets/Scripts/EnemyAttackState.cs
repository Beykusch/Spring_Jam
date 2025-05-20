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
        Transform model = agent.transform.Find("Model");
        agent.updateRotation = false;

        Vector3 direction = (player.position - agent.transform.position).normalized;
        direction.y = 0f;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            model.rotation = lookRotation; // Veya model.transform.rotation
        }

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

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.updateRotation = true;
    }

    private void LookAtPlayer()
    {
        Transform model = agent.transform.Find("Model");

        if (model == null)
        {
            Debug.LogError("Model child not found under enemy.");
            return;
        }

        Vector3 direction = (player.position - model.position).normalized;
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            model.rotation = Quaternion.RotateTowards(model.rotation, lookRotation, Time.deltaTime * 70f);
        }
    }

}
