using UnityEngine;

public class Icicle : MonoBehaviour
{
    public void UseGravity()
    {
        if (GetComponent<Rigidbody2D>() == null)
            gameObject.AddComponent<Rigidbody2D>();
        else
            return;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
    }
}
