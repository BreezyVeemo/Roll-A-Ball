using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameType { Normal, SpeedRun }
public enum WallType { Normal, Punishing }

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameType gameType;
    public WallType wallType;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //Sets the game type from our selections
    public void SetGameType(GameType _gameType)
    {
        gameType = _gameType;
    }

    public void ToggleSpeedRun(bool _speedrun)
    {
        if (_speedrun)
            SetGameType(GameType.SpeedRun);
        else
            SetGameType(GameType.Normal);
    }

    public void ToggleWallType(bool _punishing)
    {
        if (_punishing)
            wallType = WallType.Punishing;
        else
            wallType = WallType.Normal;
    }
}
