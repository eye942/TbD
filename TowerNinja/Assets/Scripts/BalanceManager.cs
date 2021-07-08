using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceManager : MonoBehaviour
{
    // Mana-related
    public static readonly int ManaStartNumber = 100;
    public static readonly int ManaMinNumber = 0;
    public static readonly int ManaMaxNumber = 999;

    public static readonly int ManaTimeRewardInterval = 10; // give reward every ? seconds
    public static readonly int ManaTimeRewardAmount = 10; // give ? mana every interval

    public static readonly int ManaFireballReward = 2;
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
    public static readonly int FireballMaxClicks = 5; // ? clicks to eliminate the fireball
    public static readonly int FireballMaxDamage = 3;
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
