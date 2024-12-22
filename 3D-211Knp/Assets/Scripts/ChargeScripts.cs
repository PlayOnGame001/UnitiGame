using UnityEngine;

public class ChargeScripts : MonoBehaviour
{
    [SerializeField]
    private float charge = 0.5f;
    [SerializeField]
    private bool isRandomCharge = false;
    private AudioSource collectSound;
    private float destroyTimeout;
    void Start()
    {
        collectSound = GetComponent<AudioSource>();
        destroyTimeout = 0f;
        GameState.Subscribe(OnSoundVolumeTrigger, "EffectsVolume");
    }

    void Update()
    {
        if (destroyTimeout > 0f)
        {
            //if (destroy)
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isRandomCharge) charge = Random.Range(0.3f, 1.0f);
        if (other.gameObject.CompareTag("Player"))
        {
            collectSound.Play();
            GameState.TriggerGameEvent("Charge", new GameEvents.MessageEvent
            {
                message = $"Вы нашли батарейку {charge:F1}",
                data = charge
            });
            Destroy(gameObject);
        }
    }

    private void OnSoundVolumeTrigger(string eventName, object data)
    {
            
    }
}
