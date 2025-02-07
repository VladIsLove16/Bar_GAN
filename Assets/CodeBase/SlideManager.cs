using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SlideManager : MonoBehaviour
{
    [SerializeField] private GameObject[] slides; // ������ �������
    [SerializeField] private float slideDuration = 0.5f; // ������������ ��������
    [SerializeField] private Button[] slideButtons; // ������ ������ ��� ��������� �������
    private int currentSlideIndex = 0; // ������ �������� ������

    private void Start()
    {
        ShowSlide(currentSlideIndex); // ���������� ������ �����

        // ����������� ������ � ������ ��� ��������� ������� �� �������
        for (int i = 0; i < slideButtons.Length; i++)
        {
            int index = i; // ��������� ������ ������
            slideButtons[i].onClick.AddListener(() => ShowSlide(index)); // ����������� �������
        }
    }

    // ����� ��� �������������� �� ��������� �����
    public void ShowSlide(int index)
    {
        if (index >= 0 && index < slides.Length)
        {
            // ��������� ������� �� ����� ������ ���� ������� ����� ���������� �� ������
            if (currentSlideIndex != index)
            {
                StartCoroutine(SlideTransition(slides[currentSlideIndex], slides[index]));
                currentSlideIndex = index; // ��������� ������� ������
            }
        }
    }

    // ������� ��� �������� �������������� �������
    private System.Collections.IEnumerator SlideTransition(GameObject oldSlide, GameObject newSlide)
    {
        // ���� ������ ����� ����������, ��������� ��� ������������
        if (oldSlide != null)
        {
            oldSlide.transform.DOLocalMoveX(-Screen.width, slideDuration).OnKill(() => oldSlide.SetActive(false)); // ������������ ������ ����� ����� ��������
        }

        // ���������� ����� �����
        newSlide.SetActive(true);
        newSlide.transform.localPosition = new Vector3(Screen.width, 0, 0); // ��������� ������� ��� ��������
        newSlide.transform.DOLocalMoveX(0, slideDuration); // ���������� ����� �� ��� �������� �������

        yield return new WaitForSeconds(slideDuration); // ���� ��������� ��������
    }
}
