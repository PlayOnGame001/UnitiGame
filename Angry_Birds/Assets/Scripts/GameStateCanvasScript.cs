using UnityEngine;

public class GameStateCanvasScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (Transform Child in transform)
        {
            Child.gameObject.SetActive(false);
        }

        GameState.OnGameOver += OnGameOver;
    }

    void OnGameOver()
    {
        foreach (Transform Child in transform)
        {
            Child.gameObject.SetActive(true);
        }
        
        GameState.OnGameOver -= OnGameOver;
    }
}
