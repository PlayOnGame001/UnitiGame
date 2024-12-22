using UnityEngine;
using UnityEngine.InputSystem;

public class PacManScripts : MonoBehaviour
{
    private Rigidbody rb;
    private InputAction moveAction;
    private Transform flashLightTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveAction = InputSystem.actions.FindAction("Move");
        flashLightTransform = transform.Find("FlashLight");
    }


    void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();

        Vector3 f = Camera.main.transform.forward;
        f.y = 0.0f;
        if( f == Vector3.zero)
        {
            f = Camera.main.transform.forward;
        }
        f.Normalize();
        Vector3 r = Camera.main.transform.right;
        r.y = 0.0f;
        r.Normalize();

        rb.AddForce(250 * Time.deltaTime * 
            (
            r * moveValue.x + 
            f * moveValue.y 
            ));

        flashLightTransform.forward = f;
/*        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        Vector2 axios = new Vector2 (
            Input.GetAxis("Horizont"),
            Input.GetAxis("Vertical"));
        if (moveValue != Vector2.zero)
        {
            Debug.Log(moveValue);
            Debug.Log(axios);
            Debug.Log("---");
        }*/
    }
}
