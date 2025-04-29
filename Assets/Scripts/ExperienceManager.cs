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
        UpdateInterface();
    }

    public void GainExperience(int amount)
    {
        currentXP += amount;
        CheckForLevelUp();
        UpdateInterface();
    }

    void CheckForLevelUp()
    {
        while (currentXP >= requiredXP)
        {
            currentXP -= requiredXP;
            currentLevel++;
            // Optional: Increase required XP per level
            // requiredXP += 50;
        }
    }

    void UpdateInterface()
    {
        levelText.text = "Level " + currentLevel;

        float targetFill = (float)currentXP / requiredXP;

        // Stop previous fill animation if it's running
        if (fillCoroutine != null)
            StopCoroutine(fillCoroutine);

        fillCoroutine = StartCoroutine(AnimateFill(targetFill));
    }

    IEnumerator AnimateFill(float targetFill)
    {
        float duration = 0.5f; // Duration of fill animation
        float startFill = experienceFill.fillAmount;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            experienceFill.fillAmount = Mathf.Lerp(startFill, targetFill, time / duration);
            yield return null;
        }

        experienceFill.fillAmount = targetFill; // Ensure it's exact
    }
}
