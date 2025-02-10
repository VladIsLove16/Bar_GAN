using System;
using UnityEngine;

public class TrayGameController : MonoBehaviour
{
    [SerializeField] DeadZone _deadZone;
    [SerializeField] ObjectOnTray[] InGameObjects;
    [SerializeField] TrayGameScoreController _scoreController;
    [SerializeField] DataManager _dataManager;
    [SerializeField] ScoreUI ScoreUI;
    private void Awake()
    {
        _deadZone.ObjectFallen += OnObjectFall;
        _scoreController.StartGame();
    }

    private void OnObjectFall(int count)
    {
        if (InGameObjects.Length == count)
            GameLose();
    }

    private void GameLose()
    {
        Debug.Log("U LOSE!");
        _dataManager.ChangeMoney(_scoreController.GetCurrentScore()/17);
        ScoreUI.ShowRestartButton();
        _scoreController.Stop();
    }
}
    
