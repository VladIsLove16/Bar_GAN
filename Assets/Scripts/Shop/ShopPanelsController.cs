using UnityEngine;

public class ShopPanelsController : MonoBehaviour
{
    [SerializeField] private ShopItemPanel StartedOpenPanel;
    private ShopItemPanel selectedPanel = null;
    private void Awake()
    {
        OpenPanel(StartedOpenPanel);
    }
    public void OpenPanel(ShopItemPanel shopItemPanel)
    {
        selectedPanel?.Close();
        selectedPanel = shopItemPanel;
        shopItemPanel.Open();
    }
}
