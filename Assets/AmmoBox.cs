using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int ammoAmount = 200;
    public AmmoType ammoType;

    public enum AmmoType
    {
        RifleAmmo,
        PistolAmmo
    }
}
