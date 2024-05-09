using UnityEngine;

public class Snowball : MonoBehaviour
{
    public GameObject EnemySnowball;

    Rigidbody2D rb2D;

    void Start()
    {
        EnemySnowball.SetActive(false);
        rb2D = EnemySnowball.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(rb2D.velocity.y == 0)
        {
            rb2D.AddForce(Vector2.left);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EnemySnowball.SetActive(true);
        }
    }
}
