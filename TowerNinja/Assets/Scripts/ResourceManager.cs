using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResourceManager : MonoBehaviour
{
    private static readonly int MaxManaNumber = 999;
    private static readonly int MinManaNumber = 0;
    private static int _currentManaNumber;
    private static float _elapsedTime; // in seconds
    private Text _manaCounterText;
    private Text _timeCounterText;
    private Text _elapsedTimeText; // gameover screen

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainGame")
        {
            _elapsedTime = 0;
            _currentManaNumber = MinManaNumber;
            GameObject manaCounter = GameObject.Find("ManaCounter").gameObject;
            _manaCounterText = manaCounter.GetComponent<Text>();
            GameObject timeCounter = GameObject.Find("TimeCounter").gameObject;
            _timeCounterText = timeCounter.GetComponent<Text>();
            UpdateManaCounterDisplay();
        }
        else if (SceneManager.GetActiveScene().name == "GameOverScreen")
        {
            GameObject elapsedTime = GameObject.Find("ElapsedTime").gameObject;
            _elapsedTimeText = elapsedTime.GetComponent<Text>();
            UpdateElapsedTimeDisplay();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainGame")
        {
            _elapsedTime += Time.deltaTime;
            UpdateTimeCounterDisplay();
        }

        // check keyboard input. for testing
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            IncreaseMana(1);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            DecreaseMana(1);
        }
    }

    public void IncreaseMana(int number)
    {
        if ((_currentManaNumber + number) <= MaxManaNumber)
        {
            _currentManaNumber += number;
            UpdateManaCounterDisplay();
        }
    }

    public void DecreaseMana(int number)
    {
        if ((_currentManaNumber - number) >= MinManaNumber)
        {
            _currentManaNumber -= number;
            UpdateManaCounterDisplay();
        }
    }

    private void UpdateManaCounterDisplay()
    {
        _manaCounterText.text = $"Mana: {_currentManaNumber}";
    }

    private void UpdateTimeCounterDisplay()
    {
        _timeCounterText.text = $"Time: {(int)_elapsedTime}";
    }

    private void UpdateElapsedTimeDisplay()
    {
        _elapsedTimeText.text = $"Elapsed Time: {(int)_elapsedTime}";
    }

}
