using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;


public class ResourceManager : MonoBehaviour
{
    private static readonly int MaxManaNumber = 999;
    private static readonly int MinManaNumber = 0;
    private static int _currentManaNumber;
    private static int _consumedManaNumber;

    private static float _elapsedTime; // in seconds
    private static float _elapsedTime75; // in seconds
    private static float _elapsedTime50; // in seconds
    private static float _elapsedTime25; // in seconds

    private Tower _tower;
    private bool _tower75HPflag;
    private bool _tower50HPflag;
    private bool _tower25HPflag;

    private Text _manaCounterText;
    private Text _timeCounterText;
    private Text _elapsedTimeText; // gameover screen

    private readonly Dictionary<string, int> _projectileDamage = new Dictionary<string, int>();

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainGame")
        {
            _elapsedTime = 0;
            _currentManaNumber = MinManaNumber;
            _consumedManaNumber = 0;
            GameObject manaCounter = GameObject.Find("ManaCounter").gameObject;
            _manaCounterText = manaCounter.GetComponent<Text>();
            GameObject timeCounter = GameObject.Find("TimeCounter").gameObject;
            _timeCounterText = timeCounter.GetComponent<Text>();
            UpdateManaCounterDisplay();

            GameObject towerObject = GameObject.Find("Tower");
            _tower = towerObject.GetComponent<Tower>();
            _tower75HPflag = false;
            _tower50HPflag = false;
            _tower25HPflag = false;
            _elapsedTime75 = 0;
            _elapsedTime50 = 0;
            _elapsedTime25 = 0;
        }
        else if (SceneManager.GetActiveScene().name == "GameOverScreen")
        {
            GameObject elapsedTime = GameObject.Find("ElapsedTime").gameObject;
            _elapsedTimeText = elapsedTime.GetComponent<Text>();
            ReportElapsedTime();
            ReportRemainingMana(_currentManaNumber);
            ReportConsumedMana(_consumedManaNumber);
            ReportProjectileDamage();
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

            // check tower health for analytics
            int towerHP = _tower.GetHealthPoint();
            if (towerHP <= 75 && _tower75HPflag == false)
            {
                _tower75HPflag = true;
                _elapsedTime75 = _elapsedTime;
            }

            if (towerHP <= 50 && _tower50HPflag == false)
            {
                _tower50HPflag = true;
                _elapsedTime50 = _elapsedTime;
            }

            if (towerHP <= 25 && _tower25HPflag == false)
            {
                _tower25HPflag = true;
                _elapsedTime25 = _elapsedTime;
            }
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
            _consumedManaNumber += number;
            UpdateManaCounterDisplay();
        }
    }

    private void UpdateManaCounterDisplay()
    {
        _manaCounterText.text = $"Mana: {_currentManaNumber}";
    }

    private void UpdateTimeCounterDisplay()
    {
        _timeCounterText.text = $"Time: {(int) _elapsedTime}";
    }

    private void UpdateElapsedTimeDisplay()
    {
        _elapsedTimeText.text = $"Elapsed Time: {(int) _elapsedTime}";
    }

    public void ReportElapsedTime()
    {
        AnalyticsEvent.Custom("elapsed_times", new Dictionary<string, object>
        {
            {"elapsed_time0", _elapsedTime},
            {"elapsed_time75", _elapsedTime75},
            {"elapsed_time50", _elapsedTime50},
            {"elapsed_time25", _elapsedTime25}
        });
        Debug.Log("Analytics - ReportElapsedTime()");
    }

    public void ReportRemainingMana(int number)
    {
        AnalyticsEvent.Custom("remaining_mana", new Dictionary<string, object>
        {
            {"remaining_mana", number}
        });
        Debug.Log("Analytics - ReportRemainingMana()");
    }

    public void ReportConsumedMana(int number)
    {
        AnalyticsEvent.Custom("consumed_mana", new Dictionary<string, object>
        {
            {"consumed_mana", number}
        });
        Debug.Log("Analytics - ReportConsumedMana()");
    }

    public void ReportProjectileDamage()
    {
        var projectileDictionary = _projectileDamage.ToDictionary(item => item.Key, value => (object) value);
        AnalyticsEvent.Custom("passive_damage", projectileDictionary);
    }

    public void UpdateProjectileDamage(string type, int damage)
    {
        if (!_projectileDamage.ContainsKey(type))
        {
            _projectileDamage.Add(type,0);
        }
       
        _projectileDamage[type] += damage;
    }
}
