using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScripts : MonoBehaviour
{
    private Transform PacMan;
    private InputAction lookAction;
    private Vector3 cameraAngles, initialCameraAngles;
    private Vector3 r;
    private float sensitivityH = 10.0f;  // Горизонтальная чувствительность
    private float sensitivityV = -10.0f; // Вертикальная чувствительность
    private float minFpvDistance = 1.0f;
    private float maxFpvDistance = 10.0f;
    private float maxCameraDistance = 5.0f; // Максимальная дистанция камеры
    private bool isPos3;

    // Угловые ограничения
    private const float FPVMinAngle = -10.0f;
    private const float FPVMaxAngle = 40.0f;
    private const float DefaultMinAngle = 35.0f;
    private const float DefaultMaxAngle = 75.0f;

    void Start()
    {
        lookAction = InputSystem.actions.FindAction("Look");
        initialCameraAngles = cameraAngles = this.transform.eulerAngles;
        PacMan = GameObject.Find("PacMan").transform;
        r = this.transform.position - PacMan.position;
        isPos3 = false;
    }

    void Update()
    {
        // Управление расстоянием камеры
        Vector2 wheel = Input.mouseScrollDelta;
        if (wheel.y < 0)
        {
            float newDistance = Mathf.Clamp(r.magnitude * (1 - wheel.y / 10), minFpvDistance, maxCameraDistance);
            r = r.normalized * newDistance;
        }

        // Управление углом камеры
        if (!isPos3)
        {
            Vector2 lookValue = lookAction.ReadValue<Vector2>();
            if (lookValue != Vector2.zero)
            {
                float minAngle, maxAngle;

                // Устанавливаем границы в зависимости от режима
                if (GameState.isFpv)
                {
                    minAngle = FPVMinAngle;
                    maxAngle = FPVMaxAngle;
                }
                else
                {
                    minAngle = DefaultMinAngle;
                    maxAngle = DefaultMaxAngle;
                }

                // Изменяем угол камеры с учетом ограничений
                cameraAngles.x = Mathf.Clamp(cameraAngles.x + lookValue.y * Time.deltaTime * sensitivityV, minAngle, maxAngle);
                cameraAngles.y += lookValue.x * Time.deltaTime * sensitivityH;

                this.transform.eulerAngles = cameraAngles;
            }

            // Обновление позиции камеры
            this.transform.position = PacMan.position +
                Quaternion.Euler(
                    cameraAngles.x - initialCameraAngles.x,
                    cameraAngles.y - initialCameraAngles.y,
                    0
                ) * r;
        }
    }
}
