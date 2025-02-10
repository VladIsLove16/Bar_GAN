using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemData" , menuName = "Shop")]
public partial class ShopItemData : ScriptableObject
{
    public string GUID;
    public Action Bought;
    public string Description => name;
    public Sprite Sprite;
    [SerializeField] private string cost;
    [SerializeField] private bool isBought;
    public bool IsBought
    {
        get
        {
            return isBought;
        }
        set
        { isBought = value; }
    }
    [SerializeField]
    private ItemType type;
    public ItemType GetItemType => type;
    private void Awake()
    {
        if (string.IsNullOrEmpty(GUID))
        {
            GUID =  Guid.NewGuid().ToString();
        }
    }
    public void Buy()
    {
        isBought = true;
        Bought?.Invoke();
    }
    public string Cost
    {
        get
        {
            try
            {
                return Convert.ToInt32(cost).ToString();
            }
            catch
            {
                return cost;
            }
        }
    }
    public ShopItemSaveData Get()
    {
       return new ShopItemSaveData(IsBought, GUID);
    }
    public void Load( ShopItemSaveData shopItemSaveData)
    {
        isBought = shopItemSaveData.isBought;
    }
}
public class ShopItemSaveData
{
    public bool isBought;
    public bool isSelected;
    public string GUID;
    public ShopItemSaveData(  bool isBought,
     string GUID)
    {
        this.isBought = isBought;
        //this.isSelected = isSelected;
        this.GUID = GUID;
         
        
    }
}