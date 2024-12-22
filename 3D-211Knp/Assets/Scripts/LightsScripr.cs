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
        // ������� ������� � ������ ��������� �����
        dayLights = GameObject.FindGameObjectsWithTag("DayLight")
            .Select(g => g.GetComponent<Light>())
            .Where(l => l != null) // ������� ������� ��� ���������� Light
            .ToArray();

        nightLights = GameObject.FindGameObjectsWithTag("NightLight")
            .Select(g => g.GetComponent<Light>())
            .Where(l => l != null) // ������� ������� ��� ���������� Light
            .ToArray();

        // �������� ��������� AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // ��������� AudioSource, ���� ��� ���
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // ��������, ��� ��������� ����� ���������
        audioSource.volume = 1.0f;

        // ��������� ���� �� ����� Resources
        if (dayNightSwitchSound == null)
        {
            dayNightSwitchSound = Resources.Load<AudioClip>("sounds/Mp3/Jingle_Win_01");
            if (dayNightSwitchSound == null)
            {
                Debug.LogError("Jingle_Win_01 not found in Resources/sounds/Mp3 folder!");
            }
        }

        // �������� ��������� ��������� �����
        SwitchLight();
    }


    void Update()
    {
        // ��������� ������� ������� N
        if (Input.GetKeyUp(KeyCode.N))
        {
            Debug.Log("N key pressed"); // ��������, ��� ������� ������� �����������
            SwitchLight();
        }
    }

    private void SwitchLight()
    {
        // ����������� ����/����
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

        // ������������� ���� ������������ ���/����
        Debug.Log("Switching lights. Playing sound."); // ��� ��� ��������
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
