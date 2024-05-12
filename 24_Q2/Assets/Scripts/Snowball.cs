using UnityEngine;

public class Snowball : MonoBehaviour
{
    Rigidbody2D rb2D;

    void Start()
    {
        gameObject.SetActive(true);
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    public void AddForce()
    {
        rb2D.AddForce(Vector2.left * 10, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
