using UnityEngine;

public class Cloud : MonoBehaviour
{
    Rigidbody2D rb2D;

    float startY;
    float moveSpeed;
    float moveDistance = 2f;

    bool ismoving;

    public bool isUp = false;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        startY = transform.position.y;
        ismoving = Random.Range(0, 2) == 0;
        moveSpeed = Random.Range(1.0f, 1.5f);
    }

    void Update()
    {
        if (!gameObject.CompareTag("Cloud"))
        {
            if (ismoving)
            {
                rb2D.velocity = Vector2.up * moveSpeed;
                if (transform.position.y >= startY + moveDistance)
                    ismoving = false;
            }
            else
            {
                rb2D.velocity = Vector2.down * moveSpeed;
                if (transform.position.y <= startY - moveDistance)
                    ismoving = true;
            }
        }
    }

    public void MoveUp()
    {
        rb2D.velocity = Vector2.up * 1.3f;
        if (transform.position.y > 10)
            gameObject.SetActive(false);
    }
}
