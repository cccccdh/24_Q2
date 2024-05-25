using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStatus
{
    GameStart,
    GameOver,
    GameClear
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 싱글톤 인스턴스
    public TextMeshProUGUI Deathcount;
    public GameObject gameOverCanvas;

    private int death = 0;

    public GameStatus status { get; private set; }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        
    }

    public GameStatus SetStatus(GameStatus newStatus)
    {
        status = newStatus;
        HandleStatusChange(newStatus);
        return status;
    }

    public void IncrementDeathCount()
    {
        death++;
        UpdateDeathCount();
    }

    private void InitializeGame()
    {
        FindGameObjects();

        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false);
        }

        UpdateDeathCount();
        SetStatus(GameStatus.GameStart);
    }

    private void UpdateDeathCount()
    {
        if (Deathcount != null)
        {
            Deathcount.text = "Death : " + death;
        }
    }

    private void HandleStatusChange(GameStatus newStatus)
    {
        switch (newStatus)
        {
            case GameStatus.GameStart:
                // Game start logic
                break;

            case GameStatus.GameOver:
                HandleGameOver();
                break;

            case GameStatus.GameClear:
                // Handle game clear logic
                break;
        }
    }

    private void HandleGameOver()
    {
        ShowGameOverScreen();
        StartCoroutine(RestartGameAfterDelay(2)); // 2초 후에 재시작
    }

    private void ShowGameOverScreen()
    {
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
        }
    }

    private IEnumerator RestartGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        SetStatus(GameStatus.GameStart);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindGameObjects();
        InitializeGame();
    }

    private void FindGameObjects()
    {
        Deathcount = GameObject.FindWithTag("Deathcount")?.GetComponent<TextMeshProUGUI>();
        gameOverCanvas = GameObject.FindWithTag("GameOverImage");
    }
}
