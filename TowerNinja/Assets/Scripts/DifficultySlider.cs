using UnityEngine;
using UnityEngine.UI;

public class DifficultySlider : MonoBehaviour
{
    public Slider mainSlider;

    private void Start()
    {
        Text text = GameObject.Find("DifficultyOptionText").GetComponent<Text>();

        if (BalanceManager.FireballMaxClicks == BalanceManager.FireballMaxClicksEasy)
        {
            mainSlider.value = 0;
        }
        else if (BalanceManager.FireballMaxClicks == BalanceManager.FireballMaxClicksNormal)
        {
            mainSlider.value = 1;
        }
        else
        {
            mainSlider.value = 2;
        }
    }

    // Invoked when the value of the slider changes.
    public void ValueChangeCheck()
    {
        Text text = GameObject.Find("DifficultyOptionText").GetComponent<Text>();

        switch (mainSlider.value)
        { 
            case 0: // easy
                BalanceManager.FireballMaxClicks = BalanceManager.FireballMaxClicksEasy;
                Debug.Log("Set difficulty to - Easy");
                text.text = "Easy";
                break;
            case 1: // normal
                BalanceManager.FireballMaxClicks = BalanceManager.FireballMaxClicksNormal;
                Debug.Log("Set difficulty to - Normal");
                text.text = "Normal";
                break;
            case 2: // hard
                BalanceManager.FireballMaxClicks = BalanceManager.FireballMaxClicksHard;
                Debug.Log("Set difficulty to - Hard");
                text.text = "Hard";
                break;
            default: // normal
                BalanceManager.FireballMaxClicks = BalanceManager.FireballMaxClicksNormal;
                break;
        }
                
    }
}
