using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteConfigManager : MonoBehaviour
{
    public int defaultLevelTime = 20;
    public int defaultCoinStartAmount = 35;
    public float defaultPlayerHealthAmount = 150f;


    public static int levelTime { get; private set; }
    public static int coinStartAmount { get; private set; }
    public static float playerHealthAmount { get; private set; }


    void Awake()
    {


        RemoteSettings.Completed += (b, b1, arg3) =>
        {
            levelTime = RemoteSettings.GetInt("levelTime", defaultLevelTime);
            coinStartAmount = RemoteSettings.GetInt("coinStartAmount", defaultCoinStartAmount);
            playerHealthAmount = RemoteSettings.GetFloat("playerHealthAmount", defaultPlayerHealthAmount);
        };
    }
}