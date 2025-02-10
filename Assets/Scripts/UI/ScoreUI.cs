using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _moneyText;
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] TextMeshProUGUI _bestScoreText;
    [SerializeField] Button _multipliersBtn1;
    [SerializeField] Button _multipliersBtn2;
    [SerializeField] Button _multipliersBtn3;
    [SerializeField] List<AnimatedText> _multipliersTexts;
    private AnimatedText _lastMultiplierText;
    [SerializeField] TrayGameScoreController _trayGameScoreController;
    [SerializeField] DataManager DataManager;
    [SerializeField] Button RestartButton;
    [SerializeField] TraySwinger _traySwinger;
    private void Awake()
    {
        _trayGameScoreController.scoreChanged += UpdateScoreText; 
        _trayGameScoreController.bestScoreChanged += UpdateBestScoreText;
        DataManager.newMoney += (int money) => _moneyText.text = money.ToString();
        _moneyText.text = DataManager.GetMoney().ToString();
        //AnimateMultiplierText(_multipliersTexts[_trayGameScoreController.GetMultiplier() - 1]);
        _lastMultiplierText = _multipliersTexts[0];
        _multipliersBtn1.onClick.AddListener(() => OnMultiplierClick(0));
        _multipliersBtn2.onClick.AddListener(() => OnMultiplierClick(1));
        _multipliersBtn3.onClick.AddListener(() => OnMultiplierClick(2));
        RestartButton.gameObject.SetActive(false);
        RestartButton.onClick.AddListener(() => SceneManager.LoadScene("GamePlayScene"));
    }   
    private void OnMultiplierClick(int i)
    {
        Debug.Log(i );
        SetScoreMultiplier(i + 1);
        AnimateMultiplierText(_multipliersTexts[i]);
    }
    private void SetScoreMultiplier(int j)
    {
        _trayGameScoreController.SetMultiplier(j);
        _traySwinger.SetSpeed(j);   

    }

    private void UpdateBestScoreText(int score)
    {
        _bestScoreText.text = score.ToString();
    }

    private void UpdateScoreText(int score)
    {
        _scoreText.text = score.ToString();
    }
    private void AnimateMultiplierText(AnimatedText text)
    {
        if(_lastMultiplierText != null)
            _lastMultiplierText.SetUnselected();
        text.SetSelected();
        _lastMultiplierText = text;

    }

    internal void ShowRestartButton()
    {
        RestartButton.gameObject.SetActive(true);
    }
}
