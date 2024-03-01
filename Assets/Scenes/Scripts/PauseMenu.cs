using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

enum GameState
{
    playing,
    paused,
    settings
}
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private string target;
    public GameObject pauseMenu;
    [SerializeField] private GameObject settingMenu;
    private GameState currentState;
    [SerializeField] private Button primaryPauseButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private TMP_Dropdown primarySettingButton;

    public void Start()
    {
        pauseMenu.SetActive(false);
        settingMenu.SetActive(false);
        currentState = GameState.playing;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (currentState)
            {
                case (GameState.playing):
                    Pause();
                    break;
                case (GameState.paused):
                    Resume();
                    break;
                case (GameState.settings):
                    CloseSettings();
                    break;
                default:
                    break;
            }
        }
    }
    
    public void Resume()
    {
        // désactiver menu pause
        pauseMenu.SetActive(false);
        // arrêter le temps
        Time.timeScale = 1;
        // changer le statut du jeu
        currentState = GameState.playing;
        
        /* Réactiver les mouvements du joueur */
    }
    public void Pause()
    {
        // activer menu pause
        pauseMenu.SetActive(true);
        // arrêter le temps
        Time.timeScale = 0;
        // changer le statut du jeu
        currentState = GameState.paused;
        
        primaryPauseButton.Select();
        /* Désactiver les mouvements du joueur */
        
    }
    
    public void LoadMainMenu()
    {
        pauseMenu.SetActive(false);
        Resume();
        SceneManager.LoadScene(target);
    }

    public void OpenSetting()
    {
        currentState = GameState.settings;
        settingMenu.SetActive(true);
        primarySettingButton.Select();
    }

    public void CloseSettings()
    {
        currentState = GameState.paused;
        settingMenu.SetActive(false);
        settingButton.Select();
    }
}