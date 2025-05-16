using UnityEngine;

public class Goon : MonoBehaviour
{
    public EnemyBullet enemyBullet;

    public int goonDamage;

    private void Start()
    {
        enemyBullet.damage = goonDamage;
    }
}
