using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class PowerUpManager : MonoBehaviour
{
    public Animator powerUpSlide;

    public AudioSource powerUpSFX;

    public GameObject middlePoint;
    public GameObject powerUpPanel;
    public GameObject expFill;
    public Button[] powerUpButtons;
    public PowerUp[] allPowerUps;

    private PowerUp[] currentChoices;

    private readonly PowerUp.PowerUpType[] typePerButton = {
        PowerUp.PowerUpType.Speedster,
        PowerUp.PowerUpType.Tank,
        PowerUp.PowerUpType.Damage
    };

    void Start()
    {
        powerUpPanel.SetActive(false);
    }
    private void Update()
    {
        if (powerUpPanel.activeSelf && powerUpSlide != null)
        {
            powerUpSlide.Update(Time.unscaledDeltaTime);
        }
    }

    public void ShowPowerUps()
    {
        powerUpSFX.Play();

        Time.timeScale = 0f;
        middlePoint.SetActive(false);
        powerUpPanel.SetActive(true);

        powerUpSlide.SetTrigger("PowerSlide");

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        currentChoices = new PowerUp[powerUpButtons.Length];

        for (int i = 0; i < powerUpButtons.Length; i++)
        {
            int index = i;
            var targetType = typePerButton[i];

            // Filter the power ups depending on the type
            var matchingPowerUps = allPowerUps.Where(p => p.type == targetType).ToArray();

            // Choose random
            PowerUp chosen = matchingPowerUps[Random.Range(0, matchingPowerUps.Length)];
            currentChoices[i] = chosen;

            // UI elements
            TMP_Text nameText = powerUpButtons[i].transform.Find("PowerUpName").GetComponent<TMP_Text>();
            TMP_Text descText = powerUpButtons[i].transform.Find("PowerUpDescription").GetComponent<TMP_Text>();
            Image iconImage = powerUpButtons[i].transform.Find("PowerUpIcon")?.GetComponent<Image>();
            Image buttonImage = powerUpButtons[i].GetComponent<Image>();

            nameText.text = chosen.powerUpName;
            descText.text = chosen.description;

            if (iconImage != null && chosen.icon != null)
            {
                iconImage.sprite = chosen.icon;
                iconImage.enabled = true;
            }

            if (buttonImage != null && chosen.buttonBackground != null)
            {
                buttonImage.sprite = chosen.buttonBackground;
                buttonImage.type = Image.Type.Sliced;
            }

            powerUpButtons[i].onClick.RemoveAllListeners();
            powerUpButtons[i].onClick.AddListener(() => SelectPowerUp(index));
        }
    }

    void SelectPowerUp(int index)
    {
        ApplyPowerUp(currentChoices[index]);
        middlePoint.SetActive(true);
        powerUpPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1f;
    }

    void ApplyPowerUp(PowerUp powerUp)
    {
        switch (powerUp.type)
        {
            case PowerUp.PowerUpType.Speedster:
                FindAnyObjectByType<PlayerMovement>().speed += powerUp.value;
                break;
            case PowerUp.PowerUpType.Tank:
                Player player = FindAnyObjectByType<Player>();
                player.HP += (int)powerUp.value;
                player.healthBar.SetHealth(player.HP);
                break;
            case PowerUp.PowerUpType.Damage:
                Weapon[] allWeapons = Resources.FindObjectsOfTypeAll<Weapon>();
                foreach (Weapon weapon in allWeapons)
                {
                    weapon.weaponDamage += (int)powerUp.value;
                }
                break;
        }
    }
}
