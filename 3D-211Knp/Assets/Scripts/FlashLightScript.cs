using UnityEngine;

public class FlashLightScript : MonoBehaviour
{
    private Transform parentTransform;
    private Light flashlight;
    private float charge;
    private float workTime = 20.0f;
    private bool played30PercentSound = false; // Флаг для 30%
    private bool played10PercentSound = false; // Флаг для 10%

    public float chargeLevel => Mathf.Clamp01(charge);

    [Header("Audio Clips")]
    public AudioClip lowCharge30Sound; // Звук при 30%
    public AudioClip lowCharge10Sound; // Звук при 10%

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

        // Инициализация AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Установка громкости
        audioSource.volume = 1.0f;
    }

    void Update()
    {
        if (parentTransform == null) return;

        if (charge > 0)
        {
            flashlight.intensity = charge;
            charge -= Time.deltaTime / workTime;

            // Проверка уровня заряда для воспроизведения звуков
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
            played30PercentSound = true; // Чтобы звук не повторялся
        }

        if (charge <= 0.1f && !played10PercentSound)
        {
            PlaySound(lowCharge10Sound);
            played10PercentSound = true; // Чтобы звук не повторялся
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

            // Сброс флагов при зарядке
            if (charge > 0.3f) played30PercentSound = false;
            if (charge > 0.1f) played10PercentSound = false;
        }
    }

    private void OnDestroy()
    {
        GameState.Unsubscribe(OnBatteryEvent, "Charge");
    }
}
