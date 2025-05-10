using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class ButtonController : MonoBehaviour
{
    public GameObject dropdownSound;

    public AudioMixer audioMixer;
    public TMP_Dropdown resDropdown;
    public AudioSource audioSource;
    public Toggle vsyncToggle;

    Resolution[] resolutions;
    void Start()
    {
        StartCoroutine(DropdownDelay());

        resolutions = Screen.resolutions;
        resDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResIndex=0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width&& resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }
        resDropdown.AddOptions(options);
        resDropdown.value = currentResIndex;
        resDropdown.RefreshShownValue();
    }
    public void WakeUp()
    {
        StartCoroutine(DelayedSceneLoad());
    }
    private IEnumerator DropdownDelay()
    {
        yield return new WaitForSeconds(0.5f);
        dropdownSound.SetActive(true);

    }
    private IEnumerator DelayedSceneLoad()
    {
        yield return new WaitForSeconds(0.6f); // Wait for 0.6 second
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Exit()
    {
        StartCoroutine(DelayedExit());
    }
    private IEnumerator DelayedExit()
    {
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }
    public void SetResolution(int resIndex)
    {
        Resolution resolution=resolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height,Screen.fullScreen);
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume",Mathf.Log10(volume)*20);
    }
    /*public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }*/
    public void SetGraphics(int graphicsIndex)
    {
        QualitySettings.SetQualityLevel(graphicsIndex);
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public void SetVsync(bool isOn)
    {
        QualitySettings.vSyncCount = vsyncToggle.isOn ? 1 : 0;
    }
}
