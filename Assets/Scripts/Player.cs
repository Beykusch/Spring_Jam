using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Animator restartAnim;

    public int HP = 100;
    public HealthBar healthBar;

    public GameObject bloodyScreen;
    public GameObject gameOver;

    public bool isDead = false;

    void Start()
    {
        healthBar.SetMaxHealth(HP);
    }
    private void Update()
    {
        if (gameOver.activeSelf && restartAnim != null)
        {
            restartAnim.Update(Time.unscaledDeltaTime);
        }
    }

    public void TakeDamage(int damageAmount)
    {

        HP -= damageAmount;
        healthBar.SetHealth(HP);

        if (HP <= 0)
        {
            print("Player Dead");
            PlayerDead();
            isDead = true;
        }
        else
        {
            print("Player Hit");
            StartCoroutine(BloodyScreenEffect());
            SoundManager.Instance.playerChannel.PlayOneShot(SoundManager.Instance.playerHurt);
        }
    }

    private void PlayerDead()
    {
        SoundManager.Instance.playerChannel.PlayOneShot(SoundManager.Instance.playerDie);
        SoundManager.Instance.playerChannel.clip = SoundManager.Instance.gameOverMusic;
        SoundManager.Instance.playerChannel.PlayDelayed(1.3f);

        GetComponent<MouseMovement>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;

        GetComponentInChildren<Animator>().enabled = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        gameOver.SetActive(true);
        StartCoroutine(StopGame());
    }

    private IEnumerator StopGame()
    {
        yield return new WaitForSeconds(1.2f);
        Time.timeScale = 0;
    }

    private IEnumerator BloodyScreenEffect()
    {
        if(bloodyScreen.activeInHierarchy == false)
        {
            bloodyScreen.SetActive(true);
        }

        var image = bloodyScreen.GetComponentInChildren<Image>();

        Color startColor = image.color;
        startColor.a = 1f;
        image.color = startColor;

        float duration = 3f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);

            Color newColor = image.color;
            newColor.a = alpha;
            image.color = newColor;

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        if (bloodyScreen.activeInHierarchy)
        {
            bloodyScreen.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            if(isDead == false)
            {
                TakeDamage(other.gameObject.GetComponent<EnemyBullet>().damage);
            }
            
        }
    }
}
