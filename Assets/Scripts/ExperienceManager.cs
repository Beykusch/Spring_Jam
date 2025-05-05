using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ExperienceManager : MonoBehaviour
{
    [Header("Experience")]
    int currentLevel = 0;
    int currentXP = 0;
    int requiredXP = 80;

    [Header("Interface")]
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] Image experienceFill;

    private float currentFillAmount = 0f;
    private Coroutine fillCoroutine;

    void Start()
    {
        
    }

    public void GainExperience(int amount)
    {
        currentXP += amount;

        bool leveledUp = CheckForLevelUp();

        UpdateInterface(leveledUp); // Only animate bar specially if level-up happened
    }

    bool CheckForLevelUp()
    {
        bool leveledUp = false;

        while (currentXP >= requiredXP)
        {
            currentXP -= requiredXP;
            currentLevel++;
            leveledUp = true;
        }

        return leveledUp;
    }

    void UpdateInterface(bool levelUpOccurred)
    {
        levelText.text = "Lv " + currentLevel;
        float targetFill = (float)currentXP / requiredXP;

        if (fillCoroutine != null)
            StopCoroutine(fillCoroutine);

        fillCoroutine = StartCoroutine(AnimateFill(targetFill, levelUpOccurred));
    }

    IEnumerator AnimateFill(float targetFill, bool levelUpOccurred)
    {
        float duration = 0.5f;
        float startFill = experienceFill.fillAmount;
        float time = 0f;

        // Animate to fill target
        while (time < duration)
        {
            time += Time.deltaTime;
            experienceFill.fillAmount = Mathf.Lerp(startFill, targetFill, time / duration);
            yield return null;
        }

        experienceFill.fillAmount = targetFill;

        if (levelUpOccurred)
        {
            // 1. Animate to full
            yield return new WaitForSeconds(0.3f);
            experienceFill.fillAmount = 1f;

            // 2. Pause while it's full
            yield return new WaitForSeconds(0.5f);

            // 3. Reset the bar visually
            experienceFill.fillAmount = 0f;

            // 4. Now show the power-up panel
            FindAnyObjectByType<PowerUpManager>().ShowPowerUps();
        }
    }
}
