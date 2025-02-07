using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DrinkSlot : MonoBehaviour
{
    [SerializeField] private Image drinkImage;
    [SerializeField] private Text drinkNameText;
    [SerializeField] private Text drinkPriceText;
    [SerializeField] private Text drinkQuantityText;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button removeButton; // ������ ��������
    [SerializeField] private Sprite sprite;
    [SerializeField] private string drinkName;
    [SerializeField] private uint price;

    [SerializeField] private Transform purchaseTarget;
    [SerializeField] private ShopManager _manager;

    [Header("VFX Settings")]
    [SerializeField] private GameObject purchaseVFXPrefab; // ������ ��� VFX �������
    [SerializeField] private Transform vfxOffset; // �������� ��� VFX

    private bool isremoved = true;
    public uint Price => price;
    public string DrinkName => drinkName;

    private void OnEnable()
    {
        if (isremoved == false)
            Destroy(gameObject);

        if (drinkImage != null)
            drinkImage.sprite = sprite;

        if (drinkNameText != null)
            drinkNameText.text = drinkName;

        if (drinkPriceText != null)
            drinkPriceText.text = "$" + price;

        if (buyButton != null)
            buyButton.onClick.AddListener(PurchaseDrink);

        if (removeButton != null)
            removeButton.onClick.AddListener(RemoveDrink);

        // ������������ ������ �������� �� ���������
        if (removeButton != null)
            removeButton.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        if (buyButton != null)
            buyButton.onClick.RemoveListener(PurchaseDrink);

        if (removeButton != null)
            removeButton.onClick.RemoveListener(RemoveDrink);
    }

    public void Setup(DrinkSlot original)
    {
        drinkImage.sprite = original.drinkImage.sprite;
        drinkNameText.text = original.DrinkName;
        drinkPriceText.text = "$" + original.Price;

        if (buyButton != null)
        {
            buyButton.gameObject.SetActive(false);
        }
    }

    public void SetQuantity(int quantity)
    {
        if (drinkQuantityText != null)
        {
            drinkQuantityText.text = "x" + quantity;
        }
    }

    public void EnableRemoveButton()
    {
        if (removeButton != null)
        {
            removeButton.gameObject.SetActive(true); // �������� ������
            removeButton.onClick.RemoveAllListeners(); // ������� ������ ��������
            removeButton.onClick.AddListener(() =>
            {
                // ���������� ��������� �� ��������
                var container = FindObjectOfType<PurchasedDrinkContainer>();
                if (container != null)
                {
                    container.AnimateAndRemoveDrink(this); // ������� � ���������
                }
            });
        }
        else
        {
            Debug.LogWarning("Remove button is not assigned in the DrinkSlot prefab!");
        }
    }

    private void PurchaseDrink()
    {
        PlayPurchaseVFX();
        // �������� ��������
        GameObject animatedCopy = Instantiate(drinkImage.gameObject, drinkImage.transform.parent);
        animatedCopy.transform.position = drinkImage.transform.position;

        animatedCopy.transform.DOMove(purchaseTarget.position, 0.5f).OnComplete(() =>
        {
            
            Destroy(animatedCopy);
            _manager.AddPurchasedDrink(this);

            // ��������������� VFX
            
        });
    }

    private void PlayPurchaseVFX()
    {
        if (purchaseVFXPrefab != null)
        {
            GameObject vfxInstance = Instantiate(purchaseVFXPrefab, vfxOffset.position, Quaternion.identity, vfxOffset);

            // ����������� VFX ������� ����� ����������
            Destroy(vfxInstance, 10f); // ����� ������� �� ������������ VFX
        }
        else
        {
            Debug.LogWarning("Purchase VFX Prefab is not assigned!");
        }
    }

    private void RemoveDrink()
    {
        isremoved = false;
        // ���������� ��������� �� �������� �������
        PurchasedDrinkContainer container = GetComponentInParent<PurchasedDrinkContainer>();

        container.AnimateAndRemoveDrink(this);
    }
}
