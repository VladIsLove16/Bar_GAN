using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal class ShopItemView : MonoBehaviour
{
    [SerializeField] Image Image;
    [SerializeField] TextMeshProUGUI Descriprion;
    [SerializeField] CostButton CostButton;
    [SerializeField] DataManager DataManager;
    internal void Init(ShopItemData item)
    {
        Image.sprite = item.Sprite;
        Descriprion.text = item.Description;
        CostButton.Init(item, DataManager);
    }

}