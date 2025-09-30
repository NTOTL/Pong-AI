using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int scorePlayer1, scorePlayer2;
    int winScore = 4;
    int winnerId;    
    public PlayMode playMode;

    // Observer pattern
    public Action OnReset; // Event to notify observers to reset the ball and paddles
    public Action<int, int> OnScore;
    public Action<int> OnGameOver;

    public enum PlayMode
    {
        PlayerVsPlayer,
        PlayerVsAI
    }


    // Singleton pattern
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("GameManager");
                _instance = obj.AddComponent<GameManager>();
                DontDestroyOnLoad(obj);
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // End of Singleton pattern


    /*
    public void OnScoreZoneReached(int zoneId)
    {
        if (zoneId == 1)
            scorePlayer1++;
        if (zoneId ==2)
            scorePlayer2++; 
    }
 */
    public void OnScoreZoneReached(string zoneTag)
    {
        SetScores(zoneTag);
        OnScore?.Invoke(scorePlayer1, scorePlayer2); // Notify observers about the score update
        if (!CheckWin())
        {
            OnReset?.Invoke(); // Notify observers to reset the ball and paddles
        }
        else
        {
            OnGameOver?.Invoke(winnerId); // Notify observers that the game is over
        }

    }



    public void OnStartGame()
    {
        winnerId = 0;
        scorePlayer1 = 0;
        scorePlayer2 = 0;
        OnReset?.Invoke(); // Notify observers to reset the ball and paddles
        OnScore?.Invoke(scorePlayer1, scorePlayer2); // Notify observers about the score update
    }

    public void SetScores(string zoneTag)
    {
        if (zoneTag == "LeftZone")
            scorePlayer2++;
        if (zoneTag == "RightZone")
            scorePlayer1++;
    }

    public bool CheckWin()
    {
        winnerId = scorePlayer1 == winScore ? 1 : scorePlayer2 == winScore ? 2 : 0;

        if (winnerId != 0)
        {
            //gameUI.OnGameWins(winnerId);
            return true;
        }
        return false;
    }

    public void SwitchPlayMode()
    {
        switch (playMode)
        {
            case PlayMode.PlayerVsPlayer:
                playMode = PlayMode.PlayerVsAI;
                break;
            case PlayMode.PlayerVsAI:
                playMode = PlayMode.PlayerVsPlayer;
                break;
        }
    }

    public bool IsPlayerVsAI()
    {
        return playMode == PlayMode.PlayerVsAI;
    }
}
