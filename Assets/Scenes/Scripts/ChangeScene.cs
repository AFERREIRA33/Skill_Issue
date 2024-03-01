using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScene : MonoBehaviour
{
    [SerializeField] private string menu;
    [SerializeField] private string game;
    
    public void LoadGame(int level)
    {
        SceneManager.LoadScene(game);
    }
    
    public void LoadMenu()
    {
        SceneManager.LoadScene(menu);
    }
}
