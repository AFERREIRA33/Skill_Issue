using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string target;
    [SerializeField] private bool isSettingsOpen;
    [SerializeField] private GameObject settingMenu;

    private void Start()
    {
        settingMenu.SetActive(false);
        isSettingsOpen = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isSettingsOpen)
        {
            CloseSettings();
        }
    }
    
    public void LoadGame()
    {
        SceneManager.LoadScene(target);
    }

    public void OpenSettings()
    {
        settingMenu.SetActive(true);
        isSettingsOpen = true;
    }
    
    public void CloseSettings()
    {
        settingMenu.SetActive(false);
        isSettingsOpen = false;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
