
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] Button playButton;
    [SerializeField] Button ShopButton;
    [SerializeField] Button BarButton;
    private void Awake()
    {
        playButton.onClick.AddListener(()=>SceneManager.LoadScene("GamePlayScene"));
        ShopButton.onClick.AddListener(()=>SceneManager.LoadScene("Shop"));
        BarButton.onClick.AddListener(()=>SceneManager.LoadScene("Shop"));
    }
}
