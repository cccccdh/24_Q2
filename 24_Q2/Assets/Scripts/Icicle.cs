using UnityEngine;

public class Icicle : MonoBehaviour
{
    public float fallSpeed = 1.0f; // 떨어지는 속도 조절 변수

    public void UseGravity()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        rb.gravityScale = fallSpeed; // 떨어지는 속도 설정
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
    }
}
