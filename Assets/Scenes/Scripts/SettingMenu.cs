using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using System.Linq;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audio;
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;
    
    // Start is called before the first frame update
    void Start()
    {
        int bestResolutionIndex = 0;
        List<string> optionsDropdowns = new List<string>();
        
        resolutions = Screen.resolutions.Select(resolution => new Resolution {width = resolution.width, height = resolution.height}).Distinct().ToArray();
        resolutionDropdown.ClearOptions();
        
        for (int index = 0; index < resolutions.Length; index++)
        {
            string option = resolutions[index].width + "x" + resolutions[index].height;
            optionsDropdowns.Add(option);

            if (resolutions[index].width == Screen.width && resolutions[index].height == Screen.height)
            {
                bestResolutionIndex = index;
            }
        }

        resolutionDropdown.AddOptions(optionsDropdowns);
        resolutionDropdown.value = bestResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    
    public void SetVolume(float value)
    {
        Debug.Log(value);
        audio.SetFloat("volume", value);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        resolutionDropdown.value = resolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
}
