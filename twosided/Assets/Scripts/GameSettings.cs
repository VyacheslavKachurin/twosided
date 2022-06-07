using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings
{
    private const string _isMusicOnKey = "IsMusicOn";
    private const string _isSoundEffectsOnKey = "IsSoundEfectsOn";
    private const string _highScoreKey = "HighScore";

    public static bool IsMusicOn
    {
        get
        {
            var value = PlayerPrefs.GetInt(_isMusicOnKey, 1);
            return value != 0;
        }

        set
        {
            PlayerPrefs.SetInt(_isMusicOnKey, value ? 1 : 0);
        }
    }

    public static bool IsSoundEffectsOn
    {
        get
        {
            var value = PlayerPrefs.GetInt(_isSoundEffectsOnKey, 1);
            return value != 0;
        }

        set
        {
            PlayerPrefs.SetInt(_isSoundEffectsOnKey, value ? 1 : 0);
        }
    }

    public static int HighScore
    {
        get
        {
            return PlayerPrefs.GetInt(_highScoreKey, 0);
        }
        set
        {
            if(value>HighScore)
            PlayerPrefs.SetInt(_highScoreKey,value);
        }
    }
}
