using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Управляет вводом от игрока и передает силу толчка в поднос.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] Button LeftSwing;
    [SerializeField] Button RightSwing;
    [Header("Настройки управления")]
    [Tooltip("Сила наклона при нажатии клавиш")]
    public float swingForce = 10f;

    private TraySwinger trayController;
    private void Awake()
    {
        LeftSwing.onClick.AddListener(()=>AddSwing(1));
        RightSwing.onClick.AddListener(()=>AddSwing(-1));
    }
    private void Start()
    {
        // Ищем TraySwinger на сцене (должен быть на подносе)
        trayController = GetComponent<TraySwinger>();
    }

    private void Update()
    {
        HandleInput();
    }
    private void AddSwing(int isRight)
    {
        trayController.AddSwing(swingForce * Time.deltaTime * isRight);
    }
    /// <summary>
    /// Обрабатывает ввод игрока.
    /// </summary>
    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("swingForce");
            AddSwing(-1);
             // Наклон влево
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            AddSwing(1); // Наклон вправо
        }
    }
}
