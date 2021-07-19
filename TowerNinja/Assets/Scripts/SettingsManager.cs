using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    public static bool AudioStateOn = true;

    void Start()
    {
        HashSet<string> scenesWithBackgroundMusic = new HashSet<string>()
        {
            "EntryScreen",
            "MainGame",
            "GameOverScreen"
        };

        if (scenesWithBackgroundMusic.Contains(SceneManager.GetActiveScene().name))
        {
            AudioSource backgroundAudio = GameObject.Find("BackgroundAudio").GetComponent<AudioSource>();
            backgroundAudio.mute = !AudioStateOn;
        }
    }
}
