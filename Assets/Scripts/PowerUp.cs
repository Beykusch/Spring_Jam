using UnityEngine;

[CreateAssetMenu(fileName = "New PowerUp", menuName = "PowerUp")]
public class PowerUp : ScriptableObject
{
    public string powerUpName;
    public string description;
    public Sprite icon;

    public enum PowerUpType { Power, Stealth, Tank }
    public PowerUpType type;
    public float value;
}
