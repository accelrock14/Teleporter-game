using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI timerText;
    private float timeElapsed = 0f;

    private GameManager gameState;

    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        gameState = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if(gameState.state != GameState.LOST)
        {
            timeElapsed += Time.deltaTime;

            int minutes = Mathf.FloorToInt(timeElapsed / 60f);
            int seconds = Mathf.FloorToInt(timeElapsed % 60f);

            string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);

            timerText.text = timerString;
        }
    }
}
