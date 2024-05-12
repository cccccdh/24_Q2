using UnityEngine;

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
        
    }

    void GameOver()
    {
        Debug.Log("æ¿ ¥ŸΩ√ Ω√¿€");
    }
}
