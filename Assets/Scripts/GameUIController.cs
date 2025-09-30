using System;
using TMPro;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    public ScoreTextController scoreTextPlayer1, scoreTextPlayer2;
    public GameObject gameMenu;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI playModeButtonText;

    private void OnEnable()
    {
        GameManager.Instance.OnScore += UpdateScoreBoard; // Subscribe to score change event
        GameManager.Instance.OnGameOver += OnGameWins;
    }

    private void Start()
    {
        AdjustPlayModeButtonText();
    }
    public void UpdateScoreBoard(int scorePlayer1, int scorePlayer2)
    {
        UpdateScores(scorePlayer1, scorePlayer2);
    }

    private void UpdateScores(int score1, int score2)
    {
        scoreTextPlayer1.SetScore(score1);
        scoreTextPlayer2.SetScore(score2);

    }

    public void OnStartGameButtonClicked()
    {
        gameMenu.SetActive(false);

        GameManager.Instance.OnStartGame();
    }

    public void OnGameWins(int winnerId)
    {
        gameMenu.SetActive(true);
        winText.text = $"Player {winnerId} wins!";
    }

    public void OnSwitchPlayModeButtonClicked()
    {
        GameManager.Instance.SwitchPlayMode();
        AdjustPlayModeButtonText();
    }

    public void AdjustPlayModeButtonText()
    {
        string s = string.Empty;
        switch (GameManager.Instance.playMode)
        {
            case GameManager.PlayMode.PlayerVsPlayer:
                s = "Player vs Player";
                break;
            case GameManager.PlayMode.PlayerVsAI:
                s = "Player vs AI";
                break;
        }
        playModeButtonText.text = s;
    }
}
