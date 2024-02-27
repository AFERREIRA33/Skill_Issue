using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    [SerializeField] private string target;
    public static bool gameIsPaused = false;
    public GameObject pauseMenu;

    public void Start()
    {
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Paused();
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
        gameIsPaused = false;
        
        /* Réactiver les mouvements du joueur */
    }
    public void Paused()
    {
        // activer menu pause
        pauseMenu.SetActive(true);
        // arrêter le temps
        Time.timeScale = 0;
        // changer le statut du jeu
        gameIsPaused = true;
        
        /* Désactiver les mouvements du joueur */
    }
    
    public void loadMainMenu()
    {
        pauseMenu.SetActive(false);
        Resume();
        SceneManager.LoadScene(target);
    }
}
