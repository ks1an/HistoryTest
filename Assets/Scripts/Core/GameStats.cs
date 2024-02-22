using System;
using UnityEngine;
using YG;

public class GameStats : MonoBehaviour
{
    [SerializeField] private GameTimer _timer;

    public static int accuracyRound = 0;
    public static string timeRound = "00:00";

    private void Start()
    {
        PlayerPrefs.GetInt("score", 0);
    }
    private void OnEnable()
    {
        GameScript.ActionGameStarted += OnGameStart;
    }

    private void OnDisable()
    {
        GameScript.ActionGameStarted -= OnGameStart;
    }

    public void GetStatsOnGameEnd(float allAnswers, float correctAnswers)
    {
        accuracyRound = Convert.ToInt16(correctAnswers / allAnswers * 100);
        GameTimer.stop = true;
        timeRound = _timer.result;

        YandexGame.savesData.record += Convert.ToInt16(correctAnswers);
        YandexGame.NewLeaderboardScores("BestPl", YandexGame.savesData.record);
        YandexGame.SaveProgress();
    }

    private void OnGameStart()
    {
        _timer.reset = true;
        GameTimer.stop = false;
    }
}
