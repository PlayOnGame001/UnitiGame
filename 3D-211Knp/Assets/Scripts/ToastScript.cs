using System.Collections.Generic;
using UnityEngine;

public class ToastScript : MonoBehaviour
{
    private TMPro.TextMeshProUGUI toastTMP;
    private static ToastScript instance;
    private float timeout = 5.0f;
    private float leftTime;
    private GameObject content;
    private readonly Queue<ToastMessage> messages = new Queue<ToastMessage>();

    public static void ShowTost(string message, float? timeout = null)
    {
        if (instance.messages.Count > 0 && 
            instance.messages.Peek().message == message)
        {
            return;
        }
        instance.messages.Enqueue(new ToastMessage
        {
            message = message,
            timeout = timeout ?? instance.timeout
        });
    }
    private void OnGameEvent(string eventName, object data)
    {
        ShowTost(eventName + " " + data);
    }

    void Start()
    {
        instance = this;
        content = transform.Find("Content").gameObject;
        toastTMP = transform.Find("Content/TostTMP").GetComponent<TMPro.TextMeshProUGUI>();
        content.SetActive(false);
        GameState.Subscribe(OnGameEvent, "KeyPoint");
    }

    void Update()
    {
        if (leftTime > 0)
        {
            leftTime -= Time.deltaTime;
            if (leftTime <= 0)
            {
                messages.Dequeue();
                content.SetActive(false);
            }
        }
        else
        {
            if (messages.Count > 0)
            {
                var m = messages.Peek();
                toastTMP.text = m.message;
                leftTime = m.timeout;
                content.SetActive(true);
            }
        }
    }

    private void OnDestroy()
    {
        GameState.Unsubscribe(OnGameEvent, "KeyPoint");
    }

    private class ToastMessage
    {
        public string message { get; set; }
        public float timeout { get; set; }
    }
}
