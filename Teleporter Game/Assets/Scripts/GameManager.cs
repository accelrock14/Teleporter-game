using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState { START, LOST}

public class GameManager : MonoBehaviour
{
    public GameState state;
    public Button button;

    // Start is called before the first frame update
    void Start()
    {
        state = GameState.START;
    }
    public void Restart()
    {
        button.gameObject.SetActive(true);
    }
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
