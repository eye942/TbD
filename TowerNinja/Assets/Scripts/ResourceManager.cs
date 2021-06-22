using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    private static readonly int MaxManaNumber = 999;
    private static readonly int MinManaNumber = 0;
    private static int _currentManaNumber;
    private static float _elapsedTime; // in seconds
    private Text _manaCounterText;
    private Text _timeCounterText;

    // Start is called before the first frame update
    void Start()
    {
        _elapsedTime = 0;
        _currentManaNumber = MinManaNumber;
        GameObject manaCounter = GameObject.Find("ManaCounter").gameObject;
        _manaCounterText = manaCounter.GetComponent<Text>();
        GameObject timeCounter = GameObject.Find("TimeCounter").gameObject;
        _timeCounterText = timeCounter.GetComponent<Text>();
        UpdateManaCounterDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        _elapsedTime += Time.deltaTime;
        UpdateTimeCounterDisplay();

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

}
