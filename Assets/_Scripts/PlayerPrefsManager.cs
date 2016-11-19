using UnityEngine;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour {

    const string MASTER_VOLUME_KEY = "master_volume";
    const string DIFFICULTY_KEY = "difficulty_key";
    const string LEVEL_KEY = "level_unlocked_";

    public static void setMasterVolume(float volume)
    {
        if (volume >= 0 && volume <= 1)
        {
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
        }

        else
        {
            Debug.Log("Volume out of bounds");
        }
    }

    public static float getMasterVolume()
    {
        return (PlayerPrefs.GetFloat(MASTER_VOLUME_KEY));
    }

    public static void unlockLevel(int level)
    {
        if(level<Application.levelCount-1)
        {
            PlayerPrefs.SetInt(LEVEL_KEY + level, 1);
        }

        else
        {
            Debug.LogError("Level out of bounds");
        }
    }
    
    public static bool isLevelUnlocked(int level)
    {
        string levelName = LEVEL_KEY + level;
        if(PlayerPrefs.GetInt(levelName)==1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public static void setDifficulty(float difficulty)
    {
        if(difficulty>=1 && difficulty<=3)
        {
            PlayerPrefs.SetFloat(DIFFICULTY_KEY, difficulty);
        }
        else
        {
            Debug.LogError("Difficulty out of range");
        }
    }

    public static float getDifficulty()
    {
        return (PlayerPrefs.GetFloat(DIFFICULTY_KEY));
    }
}
