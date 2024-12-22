using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    [SerializeField]
    private Transform rotAnchor;

    void Start()
    {
        
    }

    void Update()
    {
        float Dy = Input.GetAxis("Vertical");
        float Angle = this.transform.eulerAngles.z;
        if(Angle > 180)
        {
            Angle -= 360;
        }
        if (-15 < Angle + Dy && Angle + Dy < 60)
        {
            this.transform.RotateAround(rotAnchor.position, Vector3.forward, Dy);
        }
    }
}
/* Управління "Стрілкою"
 * 
 */
