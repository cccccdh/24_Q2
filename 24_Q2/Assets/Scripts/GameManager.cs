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
    public static GameManager instance; // ΩÃ±€≈Ê ¿ŒΩ∫≈œΩ∫

    public GameStatus status { get; private set; }

    public GameStatus SetStatus(GameStatus status)
    {
        this.status = status;
        return status;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        status = GameStatus.GameStart;
    }

    void Update()
    {
        if(status == GameStatus.GameStart)
        {
            
        }
        else if(status == GameStatus.GameOver)
        {
            Invoke("GameOver",3);
        }
        else
        {

        }
    }
        
    void GameOver()
    {
        status = GameStatus.GameStart;
        SceneManager.LoadScene("Main");
    }
}
