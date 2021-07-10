using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class SceneChanger: MonoBehaviour
{
    public static void PlayGame()
    {
        SceneManager.LoadScene("Scenes/MainGame");
        AnalyticsEvent.GameStart();
    }
    public static void GameOver()
    {
        SceneManager.LoadScene("GameOverScreen");
        AnalyticsEvent.GameOver();
    }
    public static void Settings()
    {
        SceneManager.LoadScene("SettingsScreen");
    }
    public static void Entry()
    {
        SceneManager.LoadScene("EntryScreen");
    }
}
