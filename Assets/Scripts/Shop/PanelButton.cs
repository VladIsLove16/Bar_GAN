using UnityEngine;
using UnityEngine.UI;
public class PanelButton : MonoBehaviour
{

    [Header("Animation") ]
    [SerializeField] private Sprite selectedSprite;
    [SerializeField] private Sprite unselectedSprite;
    [Header("Logic")]
    [SerializeField] private ShopItemPanel ShopItemPanel;
    [SerializeField] private ShopPanelsController ShopPanelsController;

    private Button button;

    protected void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(()=>ShopPanelsController.OpenPanel(ShopItemPanel));
        ShopItemPanel.Closed += () => button.image.sprite = unselectedSprite;
        ShopItemPanel.Opened += () => button.image.sprite = selectedSprite;
    }
}