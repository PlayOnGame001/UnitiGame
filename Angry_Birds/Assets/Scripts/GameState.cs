using UnityEngine;

public static class GameState
{
    public static bool GameOver
    {
        get => _gameOver;
        set
        {
            if (_gameOver == value)
            {
                return;
            }

            _gameOver = value;

            if (_gameOver)
            {
                OnGameOver();

                --TriesRemaining;
                PlayerPrefs.SetInt("TriesRemaining", TriesRemaining);

                _cachedTimeScale = Time.timeScale;
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = _cachedTimeScale;
            }
        }
    }

    private static bool _gameOver;

    public delegate void OnGameOverDelegate();
    public static OnGameOverDelegate OnGameOver { get; set; }

    public static int TriesRemaining { get; private set; } = 3;

    private static float _cachedTimeScale;
}