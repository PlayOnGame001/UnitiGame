using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ForceScript : MonoBehaviour
{
    private Image _indicator;
    private InputAction _moveAction;

    public float forceFactor => _indicator.fillAmount;

    void Start()
    {
        _indicator = GetComponent<Image>();
        _moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        Vector2 MoveValue = _moveAction.ReadValue<Vector2>();
        float Delta = MoveValue.x * Time.deltaTime;
        _indicator.fillAmount = Mathf.Clamp(_indicator.fillAmount + Delta, 0.1f, 1.0f); 
    }
}
