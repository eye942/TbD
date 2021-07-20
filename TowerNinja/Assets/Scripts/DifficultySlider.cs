using UnityEngine;
using UnityEngine.UI;

public class DifficultySlider : MonoBehaviour
{
    public Slider mainSlider;

    private void Start()
    {
        Text text = GameObject.Find("DifficultyOptionText").GetComponent<Text>();

        mainSlider.value = BalanceManager.Difficulty switch
        {
            BalanceManager.Level.Easy => 0,
            BalanceManager.Level.Medium => 1,
            BalanceManager.Level.Hard => 2,
            _ => 1
        };
    }

    // Invoked when the value of the slider changes.
    public void ValueChangeCheck()
    {
        Text text = GameObject.Find("DifficultyOptionText").GetComponent<Text>();

        switch (mainSlider.value)
        { 
            case 0: // easy
                BalanceManager.Difficulty = BalanceManager.Level.Easy;
                Debug.Log("Set difficulty to - Easy");
                text.text = "Easy";
                break;
            case 1: // normal
                BalanceManager.Difficulty = BalanceManager.Level.Medium;
                Debug.Log("Set difficulty to - Normal");
                text.text = "Normal";
                break;
            case 2: // hard
                BalanceManager.Difficulty = BalanceManager.Level.Hard;
                Debug.Log("Set difficulty to - Hard");
                text.text = "Hard";
                break;
            default: // normal
                BalanceManager.Difficulty = BalanceManager.Level.Medium;
                break;
        }
                
    }
}
