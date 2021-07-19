using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public static bool AudioStateOn = true;

    private static readonly HashSet<string> scenesWithBackgroundMusic = new HashSet<string>()
        {
            "EntryScreen",
            "MainGame",
            "GameOverScreen"
        };

    private static readonly HashSet<string> scenesWithGameMuteButton = new HashSet<string>()
        {
            "MainGame"
        };

    void Start()
    {
        UpdateBackgroundAudio();
    }
    private static void UpdateBackgroundAudio()
    {
        if (scenesWithBackgroundMusic.Contains(SceneManager.GetActiveScene().name))
        {
            AudioSource backgroundAudio = GameObject.Find("BackgroundAudio").GetComponent<AudioSource>();
            backgroundAudio.mute = !AudioStateOn;
        }
    }

    public static void ChangeAudioState()
    {
        AudioStateOn = !AudioStateOn;

        if (scenesWithGameMuteButton.Contains(SceneManager.GetActiveScene().name))
        {
            Text text = GameObject.Find("GameMuteButtonText").GetComponent<Text>();
            if (text.text == "Mute") text.text = "Unmute";
            else if (text.text == "Unmute") text.text = "Mute";
        }

        UpdateBackgroundAudio();
    }
}
