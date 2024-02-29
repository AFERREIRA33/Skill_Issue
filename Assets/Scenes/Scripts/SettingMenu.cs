using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using System.Linq;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer _audio;
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] _resolutions;
    
    // Start is called before the first frame update
    void Start()
    {
        int bestResolutionIndex = 0;
        List<string> optionsDropdowns = new List<string>();
        
        _resolutions = Screen.resolutions.Select(resolution => new Resolution {width = resolution.width, height = resolution.height}).Distinct().ToArray();
        resolutionDropdown.ClearOptions();
        
        for (int index = 0; index < _resolutions.Length; index++)
        {
            string option = _resolutions[index].width + "x" + _resolutions[index].height;
            optionsDropdowns.Add(option);

            if (_resolutions[index].width == Screen.width && _resolutions[index].height == Screen.height)
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
        _audio.SetFloat("volume", value);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _resolutions[resolutionIndex];
        
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        resolutionDropdown.value = resolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
}
