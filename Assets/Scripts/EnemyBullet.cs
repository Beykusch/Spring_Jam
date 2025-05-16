using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage;

    public Animator animator; // GuyEnemy'nin animator'u
    public Collider attackCollider; // EnemyBullet üzerindeki collider

    public string boolName = "isAttacking"; // Animator'daki bool parametresi

    void Update()
    {
        bool isAttacking = animator.GetBool(boolName);
        attackCollider.enabled = isAttacking;
    }

}
