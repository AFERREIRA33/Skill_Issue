using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audio;
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;
    
    // Start is called before the first frame update
    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> optionsDropdowns = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            optionsDropdowns.Add(option);
        }

        resolutionDropdown.AddOptions(optionsDropdowns);
    }

    public void setFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    
    public void setVolume(float value)
    {
        audio.SetFloat("volume", value);
        Debug.Log(value);
    }
}
