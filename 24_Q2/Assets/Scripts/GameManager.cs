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

    public GameStatus SetStatus(GameStatus status)
    {
        this.status = status;
        return status;
    }

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
        }
    }

    void Start()
    {
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false);
        }

        UpdateDeathCount();
        status = GameStatus.GameStart;
    }

    public void IncrementDeathCount()
    {
        death++;
        UpdateDeathCount();
    }

    private void UpdateDeathCount()
    {
        if (Deathcount != null)
        {
            Deathcount.text = "Death : " + death.ToString();
        }
    }

    void Update()
    {
        if (status == GameStatus.GameStart)
        {
            // Game start logic
        }
        else if (status == GameStatus.GameOver)
        {
            ShowGameOverScreen(); 
            Invoke("RestartGame", 2); // 2초 후에 재시작합니다.
        }
        else
        {
            // Other statuses
        }
    }

    private void ShowGameOverScreen()
    {
        // 게임오버 시 게임오버 캔버스를 활성화합니다.
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
        }
    }
    private void RestartGame()
    {
        // 게임을 재시작합니다. 현재 씬을 다시 로드합니다.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // 게임 오버 캔버스를 다시 비활성화합니다.
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false);
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬이 로드될 때마다 데스 카운트를 업데이트합니다.
        Deathcount = GameObject.FindWithTag("Deathcount").GetComponent<TextMeshProUGUI>();
        UpdateDeathCount();
        SetStatus(GameStatus.GameStart);
    }
}
