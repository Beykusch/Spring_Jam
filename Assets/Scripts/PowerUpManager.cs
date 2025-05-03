using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class PowerUpManager : MonoBehaviour
{
    public GameObject middlePoint;
    public GameObject powerUpPanel;
    public Button[] powerUpButtons;
    public PowerUp[] allPowerUps;

    private PowerUp[] currentChoices;

    void Start()
    {
        powerUpPanel.SetActive(false);
    }

    public void ShowPowerUps()
    {
        Time.timeScale = 0f;
        middlePoint.SetActive(false);
        powerUpPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        currentChoices = allPowerUps.OrderBy(x => Random.value).Take(3).ToArray();

        for (int i = 0; i < powerUpButtons.Length; i++)
        {
            int index = i;

            //Buttons UI elements
            TMP_Text nameText = powerUpButtons[i].transform.Find("PowerUpName").GetComponent<TMP_Text>();
            TMP_Text descText = powerUpButtons[i].transform.Find("PowerUpDescription").GetComponent<TMP_Text>();
            Image iconImage = powerUpButtons[i].transform.Find("PowerUpIcon")?.GetComponent<Image>();

            nameText.text = currentChoices[i].powerUpName;
            descText.text = currentChoices[i].description;

            if (iconImage != null && currentChoices[i].icon != null)
            {
                iconImage.sprite = currentChoices[i].icon;
                iconImage.gameObject.SetActive(true);
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
            case PowerUp.PowerUpType.Stealth:
                FindAnyObjectByType<PlayerMovement>().speed += powerUp.value;
                break;
            case PowerUp.PowerUpType.Tank:
                FindAnyObjectByType<HealthBar>().slider.value += (int)powerUp.value;
                break;
            case PowerUp.PowerUpType.Power:
                Weapon[] allWeapons = FindObjectsOfType<Weapon>();
                foreach (Weapon weapon in allWeapons)
                {
                    weapon.weaponDamage += (int)powerUp.value;
                }
                break;
        }
    }
}
