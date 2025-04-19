using UnityEngine;
using static Weapon;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }

    public AudioSource shootingSoundPistol;
    public AudioSource reloadingSoundPistol;
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
                shootingSoundPistol.Play();
                break;
            case WeaponModel.M4:
                //play M4 sound
                break;

        }
    }

    public void PlayReloadSound(WeaponModel weapon)
    {
        switch (weapon)
        {
            case WeaponModel.Pistol:    
                reloadingSoundPistol.Play();
                break;
            case WeaponModel.M4:
                //play M4 sound
                break;

        }
    }
}
