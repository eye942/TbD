using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioSource backgroundAudio = gameObject.GetComponent<AudioSource>();
        backgroundAudio.mute = !SettingsManager.AudioStateOn;
    }
}
