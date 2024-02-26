using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string target;
    
    public void loadGame()
    {
        SceneManager.LoadScene(target);
    }

    public void quitGame()
    {
        
    }
}
