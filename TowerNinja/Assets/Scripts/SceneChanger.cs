using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger: MonoBehaviour
{
    public void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    
    public void PlayGame()
    {
        SceneManager.LoadScene("Scenes/MainGame");
    }

    public void LoadScene()
    {
        Debug.Log("Clicked play game!");

        SceneManager.LoadScene("Scenes/MainGame");
    }
}
