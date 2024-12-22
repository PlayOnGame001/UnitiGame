using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContinueButtonScript : MonoBehaviour
{
    private bool _shouldRestart;
    
    [SerializeField]
    private string firstScene;
    
    [SerializeField]
    [CanBeNull]
    private string nextScene;
    
    [SerializeField]
    private GameObject textToChange;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameState.TriesRemaining <= 1)
        {
            _shouldRestart = true;

            textToChange.GetComponent<TMP_Text>().text = "Restart";
        }

        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        if (_shouldRestart || nextScene == null)
        {
            SceneManager.LoadScene(firstScene);
        }
        else
        {
            SceneManager.LoadScene(nextScene);
        }

        GameState.GameOver = false;
    }
}