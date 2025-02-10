using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TopPanelIngameUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] Button exitButton;
    private void Awake()
    {
        exitButton.onClick.AddListener(() => SceneManager.LoadScene("MainMenu"));
    }
}
