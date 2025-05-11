using UnityEngine;
using static Weapon;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }

    public AudioSource ShootingChannel;
    
    public AudioClip PistolShot;
    public AudioClip M4Shot;

    public AudioSource reloadingSoundPistol;
    public AudioSource reloadingSoundM4;

    public AudioSource enemyHit;

    

    public AudioSource emptySoundPistol;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlayShootingSound(WeaponModel weapon)
    {
        switch (weapon)
        {
            case WeaponModel.Pistol:
                ShootingChannel.PlayOneShot(PistolShot);
                break;
            case WeaponModel.M4:
                ShootingChannel.PlayOneShot(M4Shot);
                break;

        }
    }

    public void PlayEnemyHitSound()
    {
        enemyHit.PlayOneShot(enemyHit.clip);
    }

    public void PlayReloadSound(WeaponModel weapon)
    {
        switch (weapon)
        {
            case WeaponModel.Pistol:    
                reloadingSoundPistol.Play();
                break;
            case WeaponModel.M4:
                reloadingSoundM4.Play();
                break;

        }
    }
}
