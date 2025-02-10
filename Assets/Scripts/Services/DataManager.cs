using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "data",menuName = "data")]
public class DataManager : ScriptableObject
{
    [SerializeField] private ShopItemData choosedClothes;
    [SerializeField] private ShopItemData choosedHair;
    public Action<ShopItemData,ItemType> Selected;
    [SerializeField] List<ShopItemData> saveShopItems;
    private int money;
    public Action<int> newMoney;
    [ContextMenu("AddMoney")]
    public void AddMoney()
    {
        ChangeMoney(100);
    }
    public void ChangeMoney(int money)
    {
        this.money += money;
        newMoney.Invoke(this.money);
        JsonSaveSystem.Save(money.ToString(), "money");
    }
    public int GetMoney()
    {
        return this.money;
    }
    private void Awake()
    {
        string money =  JsonSaveSystem.Load<string>("money");
        try { this.money = Convert.ToInt32(money); }
        catch { Debug.LogAssertion("cant convert" + money); }
        ShopDataGlobal dataGlobal = JsonSaveSystem.Load<ShopDataGlobal>("ShopData");
        Load(choosedClothes, choosedHair);
        Application.quitting += OnApplicationQuit;
    }
    public void Load(ShopItemData clothes, ShopItemData hair )
    {
        SetHair(hair);
        SetClothes(clothes);
    }
    public void Load(ShopDataGlobal shopDataGlobal)
    {
        foreach (var Loadeditem in shopDataGlobal.shopItemSaveDatas)
        {
            var shopItem = saveShopItems.FirstOrDefault(x => x.GUID == Loadeditem.GUID);
            if (shopItem !=null)
            {
                shopItem.IsBought = Loadeditem.isBought;
                if (Loadeditem.isSelected)
                {
                    if (shopItem.GetItemType == ItemType.clothes)
                        SetClothes(shopItem);
                    else
                        SetHair(shopItem);
                }
            }
        }
    }
    public void SetHair(ShopItemData shopItemData)
    {
        choosedHair = shopItemData;
        Selected?.Invoke(shopItemData,ItemType.hair);

    }
    public void SetClothes(ShopItemData shopItemData)
    {
        choosedClothes = shopItemData;
        Selected?.Invoke(shopItemData, ItemType.clothes);
    }
    public Data GetData()
    {
        return new Data(choosedClothes, choosedHair);
    }
    public class Data
    {
        public ShopItemData choosedClothes;
        public ShopItemData choosedHair;
        public Data(ShopItemData choosedClothes, ShopItemData choosedHair)
        {
            this.choosedClothes = choosedClothes;
            this.choosedHair = choosedHair;
        }
    }
    void  OnApplicationQuit()
    {
        ShopDataGlobal shopDataGlobal = new ShopDataGlobal();
        foreach (var item in saveShopItems)
        {
            var Data = item.Get();
            if(item.GUID == choosedHair.GUID || item.GUID == choosedClothes.GUID)
            {
                Data.isSelected = true;
            }
            shopDataGlobal.shopItemSaveDatas.Add(Data);
        }
        JsonSaveSystem.Save(shopDataGlobal, "ShopData");
    }
}
[Serializable]
public class ShopDataGlobal
{
    public List<ShopItemSaveData> shopItemSaveDatas;
}
