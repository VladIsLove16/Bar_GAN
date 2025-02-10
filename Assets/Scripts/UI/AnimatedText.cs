using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedText : MonoBehaviour
{
    [SerializeField] Color selectedColor;
    [SerializeField] Color notSelectedColor;
    private TextMeshProUGUI text;
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    public void SetSelected()
    {
        text.color = selectedColor;
    }
    public void SetUnselected()
    {
        text.color = notSelectedColor;
    }
}
