using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class TrayGameScoreController : MonoBehaviour
{
    private const int FIXEDSCOREMULTIPLIER = 7;
    [SerializeField] int ScorePerSecond;
    private int currentScoreMultiplier = 1;
    public Action<int> scoreChanged;
    public Action<int> bestScoreChanged;
    private int currentScore;
    private int bestScore;
    private float lastTimeAddedScore;
    private bool isGameStarted;
    private bool isPlaying=true;
    private void Update()
    {
        if (!isPlaying)
            return;
        if (!isGameStarted)
            return;
        if (Time.time - 1f > lastTimeAddedScore)
        {
            AddSecondScore();
        }
    }
    public int GetCurrentScore()
    {
        return currentScore;
    }
    private void UpdateTimes()
    {
        lastTimeAddedScore += 1f;

    }
    public void StartGame()
    {
        lastTimeAddedScore = Time.time;
        isGameStarted = true;
        isPlaying = true;
    }
    public void SetMultiplier(int multiplier)
    {
        currentScoreMultiplier = multiplier;
    }
    private void AddSecondScore()
    {
        currentScore += currentScoreMultiplier * FIXEDSCOREMULTIPLIER;
        scoreChanged.Invoke(currentScore);
        UpdateBestScore();
        UpdateTimes();
    }

    private void UpdateBestScore()
    {
        if (bestScore < currentScore)
        { bestScore = currentScore;
            bestScoreChanged.Invoke(bestScore);
        }
    }

    internal int GetMultiplier()
    {
      return currentScoreMultiplier;
    }

    internal void Stop()
    {
        isPlaying = false;
    }
}
    
