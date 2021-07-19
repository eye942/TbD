using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    public Slider mainSlider;

    private void Start()
    {
        Text text = GameObject.Find("AudioStateText").GetComponent<Text>();

        if (SettingsManager.AudioStateOn)
        {
            mainSlider.value = 1;
            text.text = "On";
        }
        else
        {
            mainSlider.value = 0;
            text.text = "Off";
        }
    }

    // Invoked when the value of the slider changes.
    public void ValueChangeCheck()
    {
        Text text = GameObject.Find("AudioStateText").GetComponent<Text>();

        switch (mainSlider.value)
        {
            case 0: // off
                SettingsManager.AudioStateOn = false;
                Debug.Log("Set audio to - Off");
                text.text = "Off";
                break;
            case 1: // on
                SettingsManager.AudioStateOn = true;
                Debug.Log("Set audio to - On");
                text.text = "On";
                break;
            default:
                SettingsManager.AudioStateOn = true;
                Debug.Log("Set audio to - On");
                text.text = "On";
                break;
        }
    }
}
