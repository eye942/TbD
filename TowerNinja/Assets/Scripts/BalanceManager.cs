using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceManager : MonoBehaviour
{
    public enum Level
    {
        Easy,
        Medium,
        Hard
    }
    public static Level Difficulty  = Level.Medium;
    // Mana-related
    public static int ManaStartNumber
    {
        get
        {
            return Difficulty switch
            {
                Level.Hard => 50,
                _ => 100
            };
        }
    }
    public static readonly int ManaMinNumber = 0;
    public static readonly int ManaMaxNumber = 999;

    public static readonly int ManaTimeRewardInterval = 10; // give reward every ? seconds

    public static int ManaTimeRewardAmount // give ? mana every interval
    {
        get
        {
            return Difficulty switch
            {
                Level.Easy => 10,
                Level.Medium => 5,
                Level.Hard => 1,
                _ => 5
            };
        }
    }

    public static int ManaFireballReward
    {
        get
        {
            return Difficulty switch
            {
                Level.Easy => 5,
                Level.Medium => 3,
                Level.Hard => 5,
                _ => 5
            };
        }
    }
    
    public static readonly int ManaEnemyReward = 15;
    public static readonly int ManaBigEnemyReward = 20;
    public static readonly int ManaEnemySlingerReward = 25;

    public static readonly int ManaFriendCost = 20;
    public static readonly int ManaBigFriendCost = 25;
    public static readonly int ManaFriendSlingerCost = 30;

    public static readonly int ManaTestKeyboardValue = 5; // increase/decrease ? mana

    // Tower-related
    public static readonly int TowerMinHealthPoint = 0;
    public static readonly int TowerMaxHealthPoint = 100;
    public static readonly int TowerTestKeyboardValue = 7; // decrease ? HP

    // Arrow-related
    public static readonly int ArrowDamage = 25;
    public static readonly int ArrowSpawnCooldown = 1;

    // Bomb-related
    public static readonly int BombDamage = 50;
    public static readonly int BombSpawnCooldown = 3;

    // Fireball-related
    public static readonly int FireballMaxHealthPoint = 50;

    public static int FireballMaxClicks // ? clicks to eliminate the fireball
    {
        get
        {
            return Difficulty switch
            {
                Level.Easy => 1,
                Level.Medium => 5,
                Level.Hard => 10,
                _ => 5
            };
        }
    } 

    public static int FireballMaxDamage 
    {
        get
        {
            return Difficulty switch
            {
                Level.Easy => 3,
                Level.Medium => 5,
                Level.Hard => 10,
                _ => 5
            };
        }
    } 
    
    public static readonly int FireballMinDamage = 1;

    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
