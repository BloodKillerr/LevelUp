using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public GameObject MainPanel;
    public GameObject OptionsPanel;
    public Slider audioSlider;
    public Toggle fullscreenToggle;
    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionsDropdown;
    public string audioPpName;
    public string fullscreenPpName;
    private int fullscreenValue;
    Resolution[] resolutions;

    void Awake()
    {
        audioSlider.value = PlayerPrefs.GetFloat(audioPpName, 0);
        fullscreenValue = PlayerPrefs.GetInt(fullscreenPpName, 1);

        if (fullscreenValue == 1)
        {
            fullscreenToggle.isOn = true;
            Screen.fullScreen = true;
        }
        else if (fullscreenValue == 0)
        {
            fullscreenToggle.isOn = false;
            Screen.fullScreen = false;
        }
    }

    void Start()
    {
        resolutions = Screen.resolutions;

        resolutionsDropdown.ClearOptions();

        int currentResolutionIndex = 0;
        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionsDropdown.AddOptions(options);
        resolutionsDropdown.value = currentResolutionIndex;
        resolutionsDropdown.RefreshShownValue();

        MainPanel.SetActive(true);
        OptionsPanel.SetActive(false);
        audioMixer.SetFloat("volume", audioSlider.value);

        if (Screen.fullScreen == true)
        {
            PlayerPrefs.SetInt(fullscreenPpName, 1);
        }
        else if (Screen.fullScreen == false)
        {
            PlayerPrefs.SetInt(fullscreenPpName, 0);
        }
    }


    void Update()
    {
        PlayerPrefs.SetFloat(audioPpName, audioSlider.value);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;

        if (isFullscreen == true)
        {
            PlayerPrefs.SetInt(fullscreenPpName, 1);
        }
        else if (isFullscreen == false)
        {
            PlayerPrefs.SetInt(fullscreenPpName, 0);
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
