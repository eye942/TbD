using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class SceneChanger: MonoBehaviour
{
    private delegate void PlayGameDelegate();

    public static void PlayEasyGame()
    {
        BalanceManager.Difficulty = BalanceManager.Level.Easy;
        Debug.Log("Set difficulty to - Easy");
        SceneManager.LoadScene("Scenes/MainGame");
        AnalyticsEvent.GameStart();
    }
    public static void PlayNormalGame()
    {
        BalanceManager.Difficulty = BalanceManager.Level.Medium;
        Debug.Log("Set difficulty to - Normal");
        SceneManager.LoadScene("Scenes/MainGame");
        AnalyticsEvent.GameStart();
    }
    public static void PlayHardGame()
    {
        BalanceManager.Difficulty = BalanceManager.Level.Hard;
        Debug.Log("Set difficulty to - Hard");
        SceneManager.LoadScene("Scenes/MainGame");
        AnalyticsEvent.GameStart();
    }
    public static void ReplayGame()
    {

        PlayGameDelegate play = BalanceManager.Difficulty switch
        {
            BalanceManager.Level.Easy =>
                PlayEasyGame,
            BalanceManager.Level.Medium =>
                PlayNormalGame,
            BalanceManager.Level.Hard =>
                PlayHardGame,
            _ => PlayNormalGame
        };
        play();
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

    public static void PlayTutorial()
    {
        BalanceManager.Difficulty = BalanceManager.Level.Medium;
        Debug.Log("Set difficulty to - Normal");
        SceneManager.LoadScene("Scenes/Tutorial");
        AnalyticsEvent.GameStart();
    }
}
