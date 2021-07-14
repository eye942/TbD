using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class SceneChanger: MonoBehaviour
{
    public static void PlayEasyGame()
    {
        BalanceManager.FireballMaxClicks = BalanceManager.FireballMaxClicksEasy;
        Debug.Log("Set difficulty to - Easy");
        SceneManager.LoadScene("Scenes/MainGame");
        AnalyticsEvent.GameStart();
    }
    public static void PlayNormalGame()
    {
        BalanceManager.FireballMaxClicks = BalanceManager.FireballMaxClicksNormal;
        Debug.Log("Set difficulty to - Normal");
        SceneManager.LoadScene("Scenes/MainGame");
        AnalyticsEvent.GameStart();
    }
    public static void PlayHardGame()
    {
        BalanceManager.FireballMaxClicks = BalanceManager.FireballMaxClicksHard;
        Debug.Log("Set difficulty to - Hard");
        SceneManager.LoadScene("Scenes/MainGame");
        AnalyticsEvent.GameStart();
    }
    public static void ReplayGame()
    {
        if (BalanceManager.FireballMaxClicks == BalanceManager.FireballMaxClicksEasy) PlayEasyGame();
        else if (BalanceManager.FireballMaxClicks == BalanceManager.FireballMaxClicksNormal) PlayHardGame();
        else if (BalanceManager.FireballMaxClicks == BalanceManager.FireballMaxClicksHard) PlayNormalGame();
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
