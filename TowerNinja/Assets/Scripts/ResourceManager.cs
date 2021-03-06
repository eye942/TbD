using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class ResourceManager : MonoBehaviour
{
    private static int _currentManaNumber;
    private static int _consumedManaNumber;

    public static float _elapsedTime   // in seconds
    {
        get;
        private set;
    } 
    private static float _elapsedTime75; // in seconds
    private static float _elapsedTime50; // in seconds
    private static float _elapsedTime25; // in seconds

    private float _manaTimeRewardCounter;

    private Tower _tower;
    private bool _tower75HPflag;
    private bool _tower50HPflag;
    private bool _tower25HPflag;


    private float _friendlyLastTime;
    private float _slingerFriendlyLastTime;
    private float _bigFriendlyLastTime;

    private float _friendlyTime;
    private float _slingerFriendlyTime;
    private float _bigFriendlyTime;

    private Text _manaCounterText;
    private Text _timeCounterText;
    private Text _elapsedTimeText; // gameover screen
    private Text _timeGateStandardCounterText;
    private Text _timeGateBigCounterText;
    private Text _timeGateSlingerCounterText;

    private readonly Dictionary<string, int> _projectileDamage = new Dictionary<string, int>();

    // Start is called before the first frame update
    void Start()
    {


        _friendlyLastTime = -1;
        _slingerFriendlyLastTime = -1;
        _bigFriendlyLastTime = -1;

        if (SceneManager.GetActiveScene().name == "MainGame" || SceneManager.GetActiveScene().name == "HoLeeBranch")
        {
            _elapsedTime = 0;
            _currentManaNumber = BalanceManager.ManaStartNumber;

            _friendlyTime = BalanceManager.FriendlyTimeGate;
            _slingerFriendlyTime = BalanceManager.SlingerFriendlyTimeGate;
            _bigFriendlyTime = BalanceManager.BigFriendlyTimeGate;

            _consumedManaNumber = 0;
            _manaTimeRewardCounter = 0;
            GameObject manaCounter = GameObject.Find("ManaCounter");
            _manaCounterText = manaCounter.GetComponent<Text>();
            GameObject timeCounter = GameObject.Find("TimeCounter");
            _timeCounterText = timeCounter.GetComponent<Text>();
            GameObject timeGateStandardCounter = GameObject.Find("TimeGateStandardCounter");
            _timeGateStandardCounterText = timeGateStandardCounter.GetComponent<Text>();
            GameObject timeGateBigCounter = GameObject.Find("TimeGateBigCounter");
            _timeGateBigCounterText = timeGateBigCounter.GetComponent<Text>();
            GameObject timeGateSlingerCounter = GameObject.Find("TimeGateSlingerCounter");
            _timeGateSlingerCounterText = timeGateSlingerCounter.GetComponent<Text>();
            UpdateManaCounterDisplay();
            UpdateTimeGateCounterDisplay();

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
            GameObject elapsedTime = GameObject.Find("ElapsedTime");
            _elapsedTimeText = elapsedTime.GetComponent<Text>();
            ReportElapsedTime("elapsedTime0", _elapsedTime);
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
            _manaTimeRewardCounter += Time.deltaTime;
            UpdateTimeCounterDisplay();
            UpdateTimeGateCounterDisplay();
            // Time reward mana. Give ? mana per ? second
            if (_manaTimeRewardCounter > BalanceManager.ManaTimeRewardInterval)
            {
                IncreaseMana(BalanceManager.ManaTimeRewardAmount);
                _manaTimeRewardCounter = 0;
            }

            // check tower health for analytics
            int towerHP = _tower.GetHealthPoint();
            if (towerHP <= 75 && _tower75HPflag == false)
            {
                _tower75HPflag = true;
                _elapsedTime75 = _elapsedTime;
                ReportElapsedTime("elapsedTime75", _elapsedTime75);
            }

            if (towerHP <= 50 && _tower50HPflag == false)
            {
                _tower50HPflag = true;
                _elapsedTime50 = _elapsedTime;
                ReportElapsedTime("elapsedTime50", _elapsedTime50);
            }

            if (towerHP <= 25 && _tower25HPflag == false)
            {
                _tower25HPflag = true;
                _elapsedTime25 = _elapsedTime;
                ReportElapsedTime("elapsedTime25", _elapsedTime25);
            }
        }

        // check keyboard input. for testing
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            IncreaseMana(BalanceManager.ManaTestKeyboardValue);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            DecreaseMana(BalanceManager.ManaTestKeyboardValue);
        }
    }

    public void IncreaseMana(int number)
    {
        if ((_currentManaNumber + number) <= BalanceManager.ManaMaxNumber)
        {
            _currentManaNumber += number;
            UpdateManaCounterDisplay();
        }
    }

    public void DecreaseMana(int number)
    {
        if ((_currentManaNumber - number) >= BalanceManager.ManaMinNumber)
        {
            _currentManaNumber -= number;
            _consumedManaNumber += number;
            UpdateManaCounterDisplay();
        }
    }

    public int GetManaCount()
    {
        return _currentManaNumber;
    }

    private void UpdateManaCounterDisplay()
    {
        _manaCounterText.text = $"Mana: {_currentManaNumber}";
    }

    private void UpdateTimeCounterDisplay()
    {
        
        _timeCounterText.text = $"Time: {(int) _elapsedTime}";
    }
    
    private void UpdateTimeGateCounterDisplay()
    {
        _timeGateStandardCounterText.text = $"Standard: {(string) RemainingFriendlyPercent()}";
        _timeGateBigCounterText.text = $"Big: {(string) RemainingBigFriendlyPercent()}";
        _timeGateSlingerCounterText.text = $"Slinger: {(string)RemainingSlingerFriendlyPercent()}";
    }

    private void UpdateElapsedTimeDisplay()
    {
        _elapsedTimeText.text = $"Elapsed Time: {(int) _elapsedTime}";
    }

    public void ReportElapsedTime(string elapsedTimeEventName, float time)
    {
        AnalyticsEvent.Custom(elapsedTimeEventName, new Dictionary<string, object>
        {
            {elapsedTimeEventName, time}
        });
        Debug.Log($"Analytics - ReportElapsedTime({elapsedTimeEventName})");
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

    public bool SpawnFriendly(){
        if(!CanSpawnFriendly())
            return false;
        _friendlyLastTime = Time.time;  
        return true;
    }
    public bool SpawnBigFriendly(){
        if(!CanSpawnBigFriendly())
            return false;
        _bigFriendlyLastTime = Time.time;
        return true;
        
    }
    public bool SpawnSlingerFriendly(){
        if(!CanSpawnSlingerFriendly())
            return false;
        _slingerFriendlyLastTime = Time.time;
        return true;
    }


     public float RemainingFriendly(){
        if(CanSpawnFriendly())
            return _friendlyTime;
 
        return Time.time - _friendlyLastTime;
    }
    public float RemainingBigFriendly(){
        if(CanSpawnBigFriendly())
            return _bigFriendlyTime;
 
        return Time.time - _bigFriendlyLastTime;
        
    }
    public float RemainingSlingerFriendly(){
        if(CanSpawnSlingerFriendly())
            return _slingerFriendlyTime;

        return Time.time - _slingerFriendlyLastTime;
    }

     public string RemainingFriendlyPercent(){
        if(CanSpawnFriendly())
            return "100%";
         int ret = Mathf.CeilToInt( (Time.time - _friendlyLastTime)/_friendlyTime * 100.0f );
         string retstr;
         if(ret>100)
            retstr = "100%";
        else if (ret<10) {
            retstr =  ret + "%" + "  ";
        }
        else
        {
            retstr =  ret + "%" + " ";
        }
         

        return retstr;
    }
    public string RemainingBigFriendlyPercent(){
        if(CanSpawnBigFriendly())
            return "100%";
        
        int ret = Mathf.CeilToInt( (Time.time - _bigFriendlyLastTime)/_bigFriendlyTime * 100.0f );
        string retstr;
         if(ret>100)
            retstr = "100%";
        else if (ret<10) {
            retstr =  ret + "%" + "  ";
        }
        else
        {
            retstr =  ret + "%" + " ";
        }
         

        return retstr;
        
    }
    public string RemainingSlingerFriendlyPercent(){
        if(CanSpawnSlingerFriendly())
            return "100%";

        int ret = Mathf.CeilToInt( (Time.time - _slingerFriendlyLastTime)/_slingerFriendlyTime * 100.0f );
        string retstr;
         if(ret>100)
            retstr = "100%";
        else if (ret<10) {
            retstr =  ret + "%" + "  ";
        }
        else
        {
            retstr =  ret + "%" + " ";
        }
         

        return retstr;
    }


    public bool CanSpawnFriendly(){
        return _friendlyLastTime == -1 || Time.time - _friendlyLastTime > _friendlyTime;
    }
    public bool CanSpawnBigFriendly(){
        return _bigFriendlyLastTime == -1 || Time.time - _bigFriendlyLastTime > _bigFriendlyTime;
        
    }
    public bool CanSpawnSlingerFriendly(){
        return _slingerFriendlyLastTime == -1  || Time.time - _slingerFriendlyLastTime > _slingerFriendlyTime;
    }
}
