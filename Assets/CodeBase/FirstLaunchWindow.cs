using UnityEngine;

public class FirstLaunchWindow : MonoBehaviour
{
    [SerializeField] private GameObject infoWindow; // ���� ����������
    [SerializeField] private GameObject nextButton; // ������ "Next"
   // [SerializeField] private GameObject closeButton; // ������ "Close" (���� �����)

    private const string FirstLaunchKey = "FirstLaunch"; // ���� ��� ���������� ���������� � ������ �������

    private void Start()
    {
        // ���������, ��� �� ��� ������ ������
        if (PlayerPrefs.GetInt(FirstLaunchKey, 0) == 0)
        {
            // ���� ��� ������ ������, ���������� ����
            infoWindow.SetActive(true);
        }
        else
        {
            // ���� �� ������ ������, �������� ���� �����
            infoWindow.SetActive(false);
        }
    }

    // ����� ��� ������ "Next"
    public void OnNextButtonClicked()
    {
        // ���������, ��� ������ ������ ������
        PlayerPrefs.SetInt(FirstLaunchKey, 1);
        PlayerPrefs.Save();

        // ��������� ���� ����� ���� ��� ������������ ����� "Next"
        infoWindow.SetActive(false);
    }

    // ����� ��� ������ "Close" (���� �����)
    public void OnCloseButtonClicked()
    {
        // ���������, ��� ������ ������ ������
        PlayerPrefs.SetInt(FirstLaunchKey, 1);
        PlayerPrefs.Save();

        // ��������� ���� ��� ������� �� "Close"
        infoWindow.SetActive(false);
    }

    // ��������� � ���������� ������ ��� ������������
    private void ToggleButtons(bool isVisible)
    {
        nextButton.SetActive(isVisible);
        //closeButton.SetActive(isVisible);
    }
}
