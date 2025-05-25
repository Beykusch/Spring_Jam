using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int HP = 100;
    [SerializeField] private int experienceOnDeath = 10;
    public EnemyType type;

    public Collider attackCollider;

    public Transform waypointParent;
    [HideInInspector] public List<Transform> waypointsList = new List<Transform>();

    private Animator animator;

    private NavMeshAgent navAgent;


    private bool isAttacking = false;
    private bool isDead = false;

    public enum EnemyType
    {
        Special,
        Goon
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        //PlayerPrefs.SetInt("HealthEnemy", 75);
        //string x = PlayerPrefs.GetInt("HealthEnemy").ToString();
        //int health = PlayerPrefs.GetInt("HealthEnemy") - 25;
    }

    // Update is called once per frame
    public void TakeDamage(int damageAmount)
    {
        if (isDead) return;

        HP -= damageAmount;

        SoundManager.Instance.PlayEnemyHitSound();

        if (HP <= 0)
        {
            isDead = true;
            animator.SetTrigger("DIE");
            GetComponent<CapsuleCollider>().enabled = false;

            ExperienceManager xpManager = FindAnyObjectByType<ExperienceManager>();
            if (xpManager != null)
            {
                xpManager.GainExperience(experienceOnDeath);
            }
        }
        else
        {
            animator.SetTrigger("DAMAGE");
        }
    }

    private Coroutine disableRoutine;

    public void EnableAttackCollider()
    {
        if (!attackCollider.enabled)
        {
            attackCollider.enabled = true;

            // delete the previous one
            if (disableRoutine != null)
                StopCoroutine(disableRoutine);

            // auto disable the collider
            disableRoutine = StartCoroutine(AutoDisableCollider(0.3f)); // animation match
        }
    }

    private IEnumerator AutoDisableCollider(float delay)
    {
        yield return new WaitForSeconds(delay);
        attackCollider.enabled = false;
        disableRoutine = null;
    }

    public void DisableAttackCollider()
    {
        if (attackCollider.enabled)
        {
            attackCollider.enabled = false;

            if (disableRoutine != null)
            {
                StopCoroutine(disableRoutine);
                disableRoutine = null;
            }
        }
    }


    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, 18f);

    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireSphere(transform.position, 23f);

    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireSphere(transform.position, 25f);


    //}
}
