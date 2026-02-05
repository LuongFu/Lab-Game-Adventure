using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject gameWinUI;
    [SerializeField] private GameObject gamePausedUI;
    private bool isGameOver = false;
    private bool isGameWin = false;
    private bool isGamePaused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateScoreText();
        gameOverUI.SetActive(false);
        gameWinUI.SetActive(false);
        gamePausedUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.M))
        {
            if (isGamePaused)
            {
                Resume();
            }
            else
            {
                GamePause();
            }
        }
    }
    public void AddScore (int points)
    {
        if (!isGameOver && !isGameWin)
        {
            score += points;
            UpdateScoreText();
        }
    }
    private void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }
    public void GameOver()
    {
        isGameOver = true;
        score = 0;
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);
    }
    public void GameWin()
    {
        isGameWin = true;
        score = 0;
        Time.timeScale = 0f;
        gameWinUI.SetActive(true);
    }
    public void GamePause()
    {
        isGamePaused = true;
        Time.timeScale = 0f;
        gamePausedUI.SetActive(true);
    }
    public void Resume()
    {
        isGamePaused = false;
        Time.timeScale = 1f;
        gamePausedUI.SetActive(false);
    }
    public void RestartGame()
    {
        isGameOver = false;
        isGameWin = false;
        isGamePaused = false;          
        score = 0;
        UpdateScoreText();
        Time.timeScale = 1f;
        gamePausedUI.SetActive(false);
        SceneManager.LoadScene("GameScene");
    }
    public bool IsGameOver()
    {
        return isGameOver;
    }
    public bool IsGameWin()
    {
        return isGameWin;
    }
    public bool isGamePause()
    {
        return isGamePaused;
    }
    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
