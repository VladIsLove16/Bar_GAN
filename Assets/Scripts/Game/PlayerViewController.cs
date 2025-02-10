using UnityEngine;
using UnityEngine.UI;

public class PlayerViewController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer Hair;
    [SerializeField] private SpriteRenderer Clothes;
    [SerializeField] private DataManager dataManager;
    private void Awake()
    {
        Load();
    }
    public void Load()
    {
        DataManager.Data data = dataManager.GetData();
        Hair.sprite = data.choosedHair.Sprite;
        Clothes.sprite = data.choosedClothes.Sprite;
    }
}
