using System;
using UnityEngine;

public class Door1Script : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "PacMan")
        {
            ToastScript.ShowTost("��� �������� ����� �������� 9 ������ ���, �� ��� ������� ����");
            //timeout = openingTime;
        }
    }
    private 

    void Start()
    {
        
    }

    void Update()
    {
       /* if (timeout > 0f)
        {
            transform.Translate(Time.deltaTime / openingTime, 0, 0);
            timeout -= 0f;
        }*/
    }
}
