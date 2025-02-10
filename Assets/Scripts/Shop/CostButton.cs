using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ShopItemData;

public class CostButton : MonoBehaviour
{
    private const string BOGHTPHRASE = "Bought";
    private const string SELECTEDPHRASE = "Selected";

    private ShopItemData item;
    private DataManager dataManager;
    [SerializeField] private TextMeshProUGUI CostButtonText;
    [SerializeField] private Image CostButtonIcon;
    [SerializeField] private Button button;
    public void Init(ShopItemData item,DataManager dataManager)
    {
        this.item = item;
        this.dataManager = dataManager;
        if (item.Cost == "free")
        {
            CostButtonIcon.gameObject.SetActive(false);
        }
        if (item.IsBought == true)
        {
            SetBoughtState();
        }
        else
            CostButtonText.text = item.Cost.ToString();
        dataManager.Selected += OnDataManager_Selected;
        OnDataManager_Selected(dataManager.GetData().choosedHair,ItemType.hair);
        OnDataManager_Selected(dataManager.GetData().choosedClothes, ItemType.clothes);
        button.onClick.AddListener(OnClick);
    }

    private void OnDataManager_Selected(ShopItemData selection,ItemType itemType)
    {
        if (item.GetItemType != itemType)
            return;
        if (item == selection)
        {
            CostButtonIcon.gameObject.SetActive(false);
            CostButtonText.text = SELECTEDPHRASE;
        }
        else
        {
            if (item.IsBought)
            {
                SetBoughtState();
            }
        }
    }

    public void OnClick()
    {
        Debug.Log("clicked");
        if (!item.IsBought)
        {
            try
            { 
                int cost = Convert.ToInt32(item.Cost);
                if (dataManager.GetMoney() > cost)
                {
                    dataManager.ChangeMoney(-cost);
                    Buy(); 
                    Select();
                }
            }
            catch
            {
                Debug.LogError("cant convert" + item.Cost);
            }
        }
        else
            Select();
    }

    private void Select()
    {
        ItemType type = item.GetItemType;
        if (type == ItemType.hair)
            dataManager.SetHair(item);
        else
            dataManager.SetClothes(item);
    }
    private void SetBoughtState()
    {
        CostButtonIcon.gameObject.SetActive(false);
        CostButtonText.text = BOGHTPHRASE;
    }

    public void Buy()
    {
        item.Buy();
        SetBoughtState();

    }
}