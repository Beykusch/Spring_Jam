using UnityEngine;

public class Player : MonoBehaviour
{
    public int HP = 100;
    public HealthBar healthBar;


    void Start()
    {
        healthBar.SetMaxHealth(HP);
    }

    public void TakeDamage(int damageAmount)
    {

        HP -= damageAmount;
        healthBar.SetHealth(HP);

        if (HP <= 0)
        {
            print("Player Dead");
        }
        else
        {
            print("Player Hit");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            TakeDamage(other.gameObject.GetComponent<EnemyBullet>().damage);
        }
    }
}
