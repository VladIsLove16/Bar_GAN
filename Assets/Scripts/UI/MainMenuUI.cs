
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] Button playButton;
    [SerializeField] Button ShopButton;
    private void Awake()
    {
        playButton.onClick.AddListener(()=>SceneManager.LoadScene("GamePlayScene"));
        ShopButton.onClick.AddListener(()=>SceneManager.LoadScene("Shop"));
    }
}
