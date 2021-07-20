using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class SceneChanger: MonoBehaviour
{
    public void Start()
    {
        if(SceneManager.GetActiveScene().name == "MainGame")
            SetBackgroundSprite();
    }

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
        SetBackgroundSprite();
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
        else if (BalanceManager.FireballMaxClicks == BalanceManager.FireballMaxClicksNormal) PlayNormalGame();
        else if (BalanceManager.FireballMaxClicks == BalanceManager.FireballMaxClicksHard) PlayHardGame();
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
        BalanceManager.FireballMaxClicks = BalanceManager.FireballMaxClicksNormal;
        Debug.Log("Set difficulty to - Easy");
        SceneManager.LoadScene("Scenes/Tutorial");
        AnalyticsEvent.GameStart();
    }

    private static void SetBackgroundSprite()
    {
        GameObject backgroundEasy = GameObject.Find("BackgroundEasy");
        GameObject backgroundNormal = GameObject.Find("BackgroundNormal");
        GameObject backgroundHard = GameObject.Find("BackgroundHard");

        if (BalanceManager.FireballMaxClicks == BalanceManager.FireballMaxClicksEasy) // easy
        {
            backgroundEasy.SetActive(true);
            backgroundNormal.SetActive(false);
            backgroundHard.SetActive(false);
        }
        else if (BalanceManager.FireballMaxClicks == BalanceManager.FireballMaxClicksNormal) // normal
        {
            backgroundEasy.SetActive(false);
            backgroundNormal.SetActive(true);
            backgroundHard.SetActive(false);
        }
        else if (BalanceManager.FireballMaxClicks == BalanceManager.FireballMaxClicksHard) // hard
        {
            backgroundEasy.SetActive(false);
            backgroundNormal.SetActive(false);
            backgroundHard.SetActive(true);
        }
    }
}
