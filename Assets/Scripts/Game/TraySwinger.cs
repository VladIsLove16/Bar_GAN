using UnityEngine;
using System.Collections;

/// <summary>
/// Управляет раскачиванием подноса. Поднос раскачивается сам и получает внешние толчки от игрока.
/// </summary>
public class TraySwinger : MonoBehaviour
{
    [Header("Настройки раскачивания")]
    [Tooltip("Начальный угол отклонения (градусы)")]
    public float initialAmplitude = 5f;
    [Tooltip("Максимальный угол наклона")]
    public float maxAngle = 45f;
    [Tooltip("Скорость увеличения раскачивания со временем")]
    public float amplitudeGrowth = 0.2f;
    [Tooltip("Минимальный интервал смены направления (сек)")]
    public float minSwingInterval = 1f;
    [Tooltip("Максимальный интервал смены направления (сек)")]
    public float maxSwingInterval = 3f;
    [Tooltip("Затухание качаний (чем больше, тем быстрее останавливается)")]
    public float damping = 0.98f;

    private float currentAngle = 0f;    // Текущий угол поворота подноса
    private float swingVelocity = 0f;   // Скорость раскачивания
    private float currentAmplitude;     // Текущая амплитуда (увеличивается со временем)
    private Quaternion initialRotation; // Исходная ориентация подноса
    private float currentMulpiplier = 1f;

    private void Start()
    {
        initialRotation = transform.rotation;
        currentAmplitude = initialAmplitude;
        StartCoroutine(SwingRoutine()); // Запуск корутины случайных толчков
    }

    private void Update()
    {
        UpdateSwing();
    }

    /// <summary>
    /// Обновляет раскачивание подноса.
    /// </summary>
    private void UpdateSwing()
    {
        currentAngle += swingVelocity * Time.deltaTime;
        swingVelocity *= damping; // Плавное затухание
        currentAngle = Mathf.Clamp(currentAngle, -maxAngle, maxAngle);
        transform.rotation = initialRotation * Quaternion.Euler(0f, 0f, currentAngle);
    }

    /// <summary>
    /// Добавляет силу раскачивания от игрока.
    /// </summary>
    public void AddSwing(float force)
    {
        swingVelocity += force;
    }

    /// <summary>
    /// Корутина для случайных толчков подноса.
    /// </summary>
    private IEnumerator SwingRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSwingInterval, maxSwingInterval));

            float randomForce = Random.Range(-currentAmplitude, currentAmplitude);
            AddSwing(randomForce);

            // Увеличиваем амплитуду со временем
            currentAmplitude += amplitudeGrowth * currentMulpiplier;
        }
    }

    internal void SetSpeed(int j)
    {
        currentMulpiplier = j;
    }
}
