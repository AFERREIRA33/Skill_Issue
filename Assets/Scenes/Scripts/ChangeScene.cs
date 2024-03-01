using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScene : MonoBehaviour
{
    [SerializeField] private string menu;
    [SerializeField] private string game;
    
    public void LoadGame(string level)
    {
        SceneManager.LoadScene(level);
    }
    
    public void LoadMenu()
    {
        SceneManager.LoadScene(menu);
    }
}
