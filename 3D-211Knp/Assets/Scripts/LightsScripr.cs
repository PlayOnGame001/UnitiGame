using System.Linq;
using UnityEngine;

public class LightsScript : MonoBehaviour
{
    private Light[] dayLights;
    private Light[] nightLights;
    private bool isDay;

    [Header("Audio Clips")]
    public AudioClip dayNightSwitchSound;

    private AudioSource audioSource;

    void Start()
    {
        // Находим дневные и ночные источники света
        dayLights = GameObject.FindGameObjectsWithTag("DayLight")
            .Select(g => g.GetComponent<Light>())
            .Where(l => l != null) // Убираем объекты без компонента Light
            .ToArray();

        nightLights = GameObject.FindGameObjectsWithTag("NightLight")
            .Select(g => g.GetComponent<Light>())
            .Where(l => l != null) // Убираем объекты без компонента Light
            .ToArray();

        // Получаем компонент AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Добавляем AudioSource, если его нет
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Убедимся, что громкость звука настроена
        audioSource.volume = 1.0f;

        // Загружаем звук из папки Resources
        if (dayNightSwitchSound == null)
        {
            dayNightSwitchSound = Resources.Load<AudioClip>("sounds/Mp3/Jingle_Win_01");
            if (dayNightSwitchSound == null)
            {
                Debug.LogError("Jingle_Win_01 not found in Resources/sounds/Mp3 folder!");
            }
        }

        // Включаем начальное состояние света
        SwitchLight();
    }


    void Update()
    {
        // Обработка нажатия клавиши N
        if (Input.GetKeyUp(KeyCode.N))
        {
            Debug.Log("N key pressed"); // Проверка, что нажатие клавиши фиксируется
            SwitchLight();
        }
    }

    private void SwitchLight()
    {
        // Переключаем день/ночь
        GameState.isDay = !GameState.isDay;
        isDay = !isDay;

        foreach (Light light in dayLights)
        {
            light.enabled = isDay;
        }

        foreach (Light light in nightLights)
        {
            light.enabled = !isDay;
        }

        // Воспроизводим звук переключения дня/ночи
        Debug.Log("Switching lights. Playing sound."); // Лог для проверки
        PlaySound(dayNightSwitchSound);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("AudioClip is null! Please assign the sound.");
        }
    }
}
