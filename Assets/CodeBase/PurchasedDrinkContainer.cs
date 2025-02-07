using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PurchasedDrinkContainer : MonoBehaviour
{
    [SerializeField] private Transform contentParent; // �������� ��� ������
    [SerializeField] private DrinkSlot slotPrefab; // ������ �����
    [SerializeField] private float spacing = 25f; // ������ ����� �������
    [SerializeField] private RectTransform containerRect; // ���������
    [SerializeField] private ScrollRect scrollRect; // ScrollRect ��� ���������
    [SerializeField] private ShopManager _manager; // �������� ��������

    private void OnEnable()
    {
        SpawnPurchasedDrinks();
        AdjustScrollRect();
    }

    private void SpawnPurchasedDrinks()
    {
        float currentYPosition = 0f;

        // �������� ������ ���������� ��� ���������
        float containerHeight = containerRect.rect.height;

        // ������� ���������� �����
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        // �������� ��� ��������� �������
        foreach (var drinkEntry in _manager.GetPurchasedDrinks())
        {
            var drink = drinkEntry.Value.slot;
            int quantity = drinkEntry.Value.quantity;

            // ���������� �������, ������� ���� �������
            if (_manager.IsDrinkRemoved(drink.DrinkName))
            {
                continue;
            }

            // ������� ����� ���� ��� �������
            DrinkSlot newSlot = Instantiate(slotPrefab, contentParent);
            newSlot.Setup(drink);
            newSlot.SetQuantity(quantity);
            newSlot.EnableRemoveButton();

            // ������������� ������� �����
            RectTransform slotRect = newSlot.GetComponent<RectTransform>();
            slotRect.anchoredPosition = new Vector2(0, currentYPosition);

            currentYPosition -= slotRect.rect.height + spacing;
        }

        // ��������� ������ �������� ��� ����� ���������� ������
        float contentHeight = Mathf.Max(-currentYPosition, containerHeight);
        containerRect.sizeDelta = new Vector2(containerRect.sizeDelta.x, contentHeight);
    }

    private void AdjustScrollRect()
    {
        // ��������, ��� ScrollRect �������� ���������
        if (scrollRect != null && containerRect != null)
        {
            scrollRect.content = containerRect;
            scrollRect.vertical = true; // �������� ������������ ���������
            scrollRect.horizontal = false; // ��������� �������������� ���������
        }
    }

    public void UpdateContainer()
    {
        SpawnPurchasedDrinks();
        AdjustScrollRect();
    } 

    public void AnimateAndRemoveDrink(DrinkSlot drinkSlot)
    {
        // ��� �������� ������������� � �������� ��������
        drinkSlot.transform.DOMove(contentParent.position, 0.3f).OnComplete(() =>
        {
            Destroy(drinkSlot.gameObject); // ������� ����
            _manager.RemovePurchasedDrink(drinkSlot.DrinkName); // ������� ������� �� ���������
           // UpdateContainer(); // �������� ���������
        });
    }
}
