using UnityEngine;

public class BirdScript : MonoBehaviour
{
    [SerializeField]
    private Transform arrow;

    private Rigidbody2D _rb2d;
    private ForceScript _forceScript;

    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _forceScript = 
            GameObject
            .Find("ForceCanvasIndicator")
            .GetComponent<ForceScript>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float ForceFactor = 1000.0f;
            if (_forceScript != null)
            {
                ForceFactor *= _forceScript.forceFactor + 0.5f;
            }
            else
            {
                Debug.LogError("forceScript NULL, used default");
            }
            _rb2d.AddForce(ForceFactor * arrow.right);
        }
    }
}
/* Д.З. Створити додаткову сцену
 * Використати іншого персонажа (жовтий)
 * Розмістити стартову позицію в іншому місці
 * Підібрати та встановити обмеження для напряму початкового руху (Стрілки)
 */
