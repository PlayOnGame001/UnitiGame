using UnityEngine;

public class FlashLightScript : MonoBehaviour
{
    private Transform parentTransform;
    private Light flashlight;
    private float charge;
    private float workTime = 20.0f;
    private bool played30PercentSound = false; // ���� ��� 30%
    private bool played10PercentSound = false; // ���� ��� 10%

    public float chargeLevel => Mathf.Clamp01(charge);

    [Header("Audio Clips")]
    public AudioClip lowCharge30Sound; // ���� ��� 30%
    public AudioClip lowCharge10Sound; // ���� ��� 10%

    private AudioSource audioSource;

    void Start()
    {
        parentTransform = transform.parent;
        if (parentTransform == null)
        {
            Debug.LogError("FlashLightScript: parentTransform not found");
        }

        flashlight = GetComponent<Light>();
        charge = 1.0f;

        GameState.Subscribe(OnBatteryEvent, "Charge");

        // ������������� AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // ��������� ���������
        audioSource.volume = 1.0f;
    }

    void Update()
    {
        if (parentTransform == null) return;

        if (charge > 0)
        {
            flashlight.intensity = charge;
            charge -= Time.deltaTime / workTime;

            // �������� ������ ������ ��� ��������������� ������
            CheckChargeLevel();
        }

        if (GameState.isFpv)
        {
            transform.forward = Camera.main.transform.forward;
        }
        else
        {
            Vector3 f = Camera.main.transform.forward;
            f.y = 0.0f;
            if (f == Vector3.zero) f = Camera.main.transform.up;
            transform.forward = f.normalized;
        }
    }

    private void CheckChargeLevel()
    {
        if (charge <= 0.3f && !played30PercentSound)
        {
            PlaySound(lowCharge30Sound);
            played30PercentSound = true; // ����� ���� �� ����������
        }

        if (charge <= 0.1f && !played10PercentSound)
        {
            PlaySound(lowCharge10Sound);
            played10PercentSound = true; // ����� ���� �� ����������
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("AudioClip is null! Please assign the sound.");
            return;
        }

        if (audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    private void OnBatteryEvent(string eventName, object data)
    {
        if (data is GameEvents.MessageEvent e)
        {
            charge += (float)e.data;

            // ����� ������ ��� �������
            if (charge > 0.3f) played30PercentSound = false;
            if (charge > 0.1f) played10PercentSound = false;
        }
    }

    private void OnDestroy()
    {
        GameState.Unsubscribe(OnBatteryEvent, "Charge");
    }
}
