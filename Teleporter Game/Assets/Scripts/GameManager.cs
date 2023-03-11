using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { START, LOST}

public class GameManager : MonoBehaviour
{
    public GameState state;
    public GameObject menu;

    // Start is called before the first frame update
    void Start()
    {
        state = GameState.START;
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Restart()
    {
        menu.gameObject.SetActive(true);
        AudioManager.bgInstance.bgMusic.Stop();
    }
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        AudioManager.bgInstance.bgMusic.Play();
    }
}
