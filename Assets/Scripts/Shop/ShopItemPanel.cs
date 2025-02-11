using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemPanel : MonoBehaviour
{
    [SerializeField] List<ShopItemData> Data;
    [SerializeField] ShopItemView prefab;
    public Action Closed;
    public Action Opened;

    private void Start()
    {
        foreach (ShopItemData item in Data)
        {
            ShopItemView view = Instantiate(prefab,transform);
            view.Init(item);
        }

    }
    public void Open()
    {
        gameObject.SetActive(true);
        Opened?.Invoke();
    }
    public void Close()
    {
        gameObject.SetActive(false);
        Closed?.Invoke();
    }
}
