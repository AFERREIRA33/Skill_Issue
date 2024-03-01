using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string target;
    [SerializeField] private bool isSettingsOpen;
    [SerializeField] private GameObject settingMenu;
    [SerializeField] private Button primaryMenuButton; 
    [SerializeField] private Button SettingButton;
    [SerializeField] private TMP_Dropdown primarySettingButton;
    private MenuInput _mInput;

    private void Start()
    {
        settingMenu.SetActive(true);
        isSettingsOpen = true;
        _mInput = new MenuInput();
        _mInput.Enable();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isSettingsOpen)
        {
            CloseSettings();
        }
    }
    
    public void LoadMapChoice()
    {
        SceneManager.LoadScene(target);
    }

    public void OpenSettings()
    {
        settingMenu.SetActive(true);
        isSettingsOpen = true;
        SettingButton.Select();
    }
    
    public void CloseSettings()
    {
        settingMenu.SetActive(false);
        isSettingsOpen = false;
        primaryMenuButton.Select();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}