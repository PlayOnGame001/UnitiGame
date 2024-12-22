using UnityEngine;

public class ClockScript : MonoBehaviour
{
    private TMPro.TextMeshProUGUI _clock;
    private float _gameTime;

    [SerializeField]
    private float gameTimeLimit = 15.0f;

    void Start()
    {
        _clock = GetComponent<TMPro.TextMeshProUGUI>();
        _gameTime = gameTimeLimit;
    }

    void Update()
    {
        _gameTime = Mathf.Clamp(_gameTime - Time.deltaTime, 0, gameTimeLimit);
        _clock.text = _gameTime.ToString("F2");

        if (_gameTime == 0)
        {
            GameState.GameOver = true;
            _gameTime = gameTimeLimit;
        }
    }
}