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
    public static GameManager instance; // �̱��� �ν��Ͻ�
    public TextMeshProUGUI Deathcount;
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
            Invoke("GameOver", 3);
        }
        else
        {
            // Other statuses
        }
    }

    private void GameOver()
    {
        // Game over logic
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ���� �ε�� ������ ���� ī��Ʈ�� ������Ʈ�մϴ�.
        Deathcount = GameObject.FindWithTag("Deathcount").GetComponent<TextMeshProUGUI>();
        UpdateDeathCount();
        SetStatus(GameStatus.GameStart);
    }
}
