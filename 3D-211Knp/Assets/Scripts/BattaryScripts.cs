using UnityEngine;
using UnityEngine.UI;

public class BattaryScripts : MonoBehaviour
{
    private Image image;
    private FlashLightScript flashLightScript;

    void Start()
    {
        image = GetComponent<Image>();
        flashLightScript = GameObject
            .Find("FlashLight")
            .GetComponent<FlashLightScript>();
    }

    void Update()
    {
        image.fillAmount = flashLightScript.chargeLevel;
        if (image.fillAmount > 0.8f)
        {
            image.color = Color.green;
        }
        else if (image.fillAmount > 0.3f)
        {
            image.color = Color.yellow;
        }
        else
        {
            image .color = Color.red;
        }
            
    }
}
