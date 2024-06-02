using UnityEngine;

public class Icicle : MonoBehaviour
{
    public float fallSpeed = 1.0f; // �������� �ӵ� ���� ����

    public void UseGravity()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        rb.gravityScale = fallSpeed; // �������� �ӵ� ����
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
    }
}
