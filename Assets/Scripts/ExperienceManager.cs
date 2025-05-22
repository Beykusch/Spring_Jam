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
    [SerializeField] GameObject powerUpPanel;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] Image experienceFill;

    bool isAnimating = false;
    private float currentFillAmount = 0f;
    private Coroutine fillCoroutine;

    void Start()
    {
        
    }

    public void GainExperience(int amount)
    {
        currentXP += amount;

        if (fillCoroutine != null)
            StopCoroutine(fillCoroutine);

        fillCoroutine = StartCoroutine(FillAndCheckXP());
    }

    IEnumerator FillAndCheckXP()
    {
        isAnimating = true;

        while (currentXP >= requiredXP)
        {
            // Step 1: Animate to full
            yield return AnimateFill(1f);

            // Step 2: Pause briefly to show full bar
            yield return new WaitForSeconds(0.2f);

            // Step 3: Level up
            currentXP -= requiredXP;
            currentLevel++;
            levelText.text = "" + currentLevel;

            // Step 4: Reset bar visually
            experienceFill.fillAmount = 0f;

            // Step 5: Show panel
            FindAnyObjectByType<PowerUpManager>().ShowPowerUps();

            // Step 6 (optional): Wait until panel closes if it's modal
            yield return new WaitUntil(() => !powerUpPanel.activeInHierarchy);
        }

        // Animate to remaining XP (non-level-up)
        float targetFill = (float)currentXP / requiredXP;
        yield return AnimateFill(targetFill);

        isAnimating = false;
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


    IEnumerator AnimateFill(float target)
    {
        float duration = 0.4f;
        float start = experienceFill.fillAmount;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            experienceFill.fillAmount = Mathf.Lerp(start, target, time / duration);
            yield return null;
        }

        experienceFill.fillAmount = target;
    }
}
