using UnityEngine;
using UnityEngine.UI;

public class SettingScript : MonoBehaviour
{
    private GameObject content;

    private Slider effectsSlider;
    private Slider ambientSlider;
    private Slider musicSlider;
    private Slider fpvSlider;

    private Slider sensitivityXSlider;
    private Slider sensitivityYSlider;
    private Toggle linkToggle;

    private Button saveButton;
    private Button closeButton;
    private Button cancelButton;

    void Start()
    {
        Transform contentTransform = transform.Find("Content");
        content = contentTransform.gameObject;

        if (content.activeInHierarchy)
        {
            Time.timeScale = 0.0f;
        }

        // Инициализация слайдеров
        effectsSlider = contentTransform.Find("Sound/SoundSlider")?.GetComponent<Slider>();
        ambientSlider = contentTransform.Find("Sound/BackSound")?.GetComponent<Slider>();
        musicSlider = contentTransform.Find("Sound/MusicSlider")?.GetComponent<Slider>();
        fpvSlider = contentTransform.Find("Controls/FpvSlider")?.GetComponent<Slider>();
        sensitivityXSlider = contentTransform.Find("Control/X_Controller")?.GetComponent<Slider>();
        sensitivityYSlider = contentTransform.Find("Control/Y_Controller")?.GetComponent<Slider>();
        linkToggle = contentTransform.Find("Control/LinkToggle")?.GetComponent<Toggle>();

        // Инициализация кнопок
        saveButton = contentTransform.Find("SaveButton")?.GetComponent<Button>();
        closeButton = contentTransform.Find("CloseButton")?.GetComponent<Button>();
        cancelButton = contentTransform.Find("CancelButton")?.GetComponent<Button>();

        if (saveButton == null || closeButton == null || cancelButton == null)
        {
            Debug.LogError("One or more buttons are not found in the canvas.");
            return;
        }

        // Добавление обработчиков событий для кнопок
        saveButton.onClick.AddListener(OnSaveButtonClick);
        closeButton.onClick.AddListener(OnCloseButtonClick);
        cancelButton.onClick.AddListener(OnCancelButtonClick);

        // Загрузка настроек
        LoadSettings();
    }

    void Update()
    {
        // Открытие/закрытие меню при нажатии на клавишу Esc
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (content != null)
            {
                Time.timeScale = content.activeInHierarchy ? 1.0f : 0.0f;
                content.SetActive(!content.activeInHierarchy); 
            }
        }
    }


    private void LoadSettings()
    {
        if (effectsSlider != null && PlayerPrefs.HasKey(nameof(effectsSlider)))
        {
            effectsSlider.value = PlayerPrefs.GetFloat(nameof(effectsSlider), 1.0f);
        }
        else
        {
            Debug.LogError("effectsSlider is not assigned or no saved value found.");
        }

        if (ambientSlider != null && PlayerPrefs.HasKey(nameof(ambientSlider)))
        {
            ambientSlider.value = PlayerPrefs.GetFloat(nameof(ambientSlider), 1.0f);
        }
        else
        {
            Debug.LogError("ambientSlider is not assigned or no saved value found.");
        }

        if (musicSlider != null && PlayerPrefs.HasKey(nameof(musicSlider)))
        {
            musicSlider.value = PlayerPrefs.GetFloat(nameof(musicSlider), 1.0f);
        }
        else
        {
            Debug.LogError("musicSlider is not assigned or no saved value found.");
        }

        if (fpvSlider != null && PlayerPrefs.HasKey(nameof(fpvSlider)))
        {
            fpvSlider.value = PlayerPrefs.GetFloat(nameof(fpvSlider), 0.5f);
        }
        else
        {
            Debug.LogError("fpvSlider is not assigned or no saved value found.");
        }

        if (sensitivityXSlider != null && PlayerPrefs.HasKey(nameof(sensitivityXSlider)))
        {
            sensitivityXSlider.value = PlayerPrefs.GetFloat(nameof(sensitivityXSlider), 1.0f);
        }
        else
        {
            Debug.LogError("sensitivityXSlider is not assigned or no saved value found.");
        }

        if (sensitivityYSlider != null && PlayerPrefs.HasKey(nameof(sensitivityYSlider)))
        {
            sensitivityYSlider.value = PlayerPrefs.GetFloat(nameof(sensitivityYSlider), 1.0f);
        }
        else
        {
            Debug.LogError("sensitivityYSlider is not assigned or no saved value found.");
        }

        if (linkToggle != null && PlayerPrefs.HasKey(nameof(linkToggle)))
        {
            linkToggle.isOn = PlayerPrefs.GetInt(nameof(linkToggle), 0) == 1;
        }
        else
        {
            Debug.LogError("linkToggle is not assigned or no saved value found.");
        }
    }

    private void SaveInitialValues()
    {
        if (effectsSlider != null)
            PlayerPrefs.SetFloat(nameof(effectsSlider), effectsSlider.value);

        if (ambientSlider != null)
            PlayerPrefs.SetFloat(nameof(ambientSlider), ambientSlider.value);

        if (musicSlider != null)
            PlayerPrefs.SetFloat(nameof(musicSlider), musicSlider.value);

        if (fpvSlider != null)
            PlayerPrefs.SetFloat(nameof(fpvSlider), fpvSlider.value);

        if (sensitivityXSlider != null)
            PlayerPrefs.SetFloat(nameof(sensitivityXSlider), sensitivityXSlider.value);

        if (sensitivityYSlider != null)
            PlayerPrefs.SetFloat(nameof(sensitivityYSlider), sensitivityYSlider.value);

        if (linkToggle != null)
            PlayerPrefs.SetInt(nameof(linkToggle), linkToggle.isOn ? 1 : 0);

        PlayerPrefs.Save();
    }

    private void OnSaveButtonClick()
    {
        SaveInitialValues();
        Debug.Log("Settings saved!");
    }

    private void OnCloseButtonClick()
    {
        content.SetActive(false);
        Time.timeScale = 1.0f;  // Возвращаем обычную скорость игры
    }

    private void OnCancelButtonClick()
    {
        RestoreInitialValues();
        content.SetActive(false);
        Time.timeScale = 1.0f;  // Возвращаем обычную скорость игры
    }

    private void RestoreInitialValues()
    {
        if (effectsSlider != null)
            effectsSlider.value = PlayerPrefs.GetFloat(nameof(effectsSlider), 1.0f);

        if (ambientSlider != null)
            ambientSlider.value = PlayerPrefs.GetFloat(nameof(ambientSlider), 1.0f);

        if (musicSlider != null)
            musicSlider.value = PlayerPrefs.GetFloat(nameof(musicSlider), 1.0f);

        if (fpvSlider != null)
            fpvSlider.value = PlayerPrefs.GetFloat(nameof(fpvSlider), 0.5f);

        if (sensitivityXSlider != null)
            sensitivityXSlider.value = PlayerPrefs.GetFloat(nameof(sensitivityXSlider), 1.0f);

        if (sensitivityYSlider != null)
            sensitivityYSlider.value = PlayerPrefs.GetFloat(nameof(sensitivityYSlider), 1.0f);

        if (linkToggle != null)
            linkToggle.isOn = PlayerPrefs.GetInt(nameof(linkToggle), 0) == 1;
    }

    public void OnFpvSliderChanged(float value)
    {
        GameState.minFpvDistance = Mathf.Lerp(0.5f, 1.5f, value);
    }

    public void OnSensitivityXSliderChanged(float value)
    {
        if (linkToggle.isOn)
        {
            sensitivityYSlider.value = value;
        }
    }

    public void OnSensitivityYSliderChanged(float value)
    {
        if (linkToggle.isOn)
        {
            sensitivityXSlider.value = value;
        }
    }

    public void OnEffectsSliderChanged(float value)
    {
        GameState.effectsVolume = value;
        GameState.TriggerGameEvent("EffectsVolume", GameState.effectsVolume = value);
    }

    public void OnAmbientSliderChanged(float value)
    {
        GameState.ambientVolume = value;
        GameState.TriggerGameEvent("EffectsVolume", GameState.ambientVolume = value);
    }

    public void OnMusicSliderChanged(float value)
    {
        GameState.musicVolume = value;
        GameState.TriggerGameEvent("EffectsVolume", GameState.musicVolume = value);
    }
}
