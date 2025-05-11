using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int HP = 100;
    [SerializeField] private int experienceOnDeath = 10;
    public EnemyType type;

    private Animator animator;

    private NavMeshAgent navAgent;

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

    private void Update()
    {
        if(navAgent.velocity.magnitude > 0.1f)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

    }
}
