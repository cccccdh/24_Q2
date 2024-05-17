using UnityEngine;

public class Questionbox : MonoBehaviour
{
    public GameObject fish;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("플레이어 닿음");
            fish.SetActive(true);
            fish.AddComponent<Rigidbody2D>();
            fish.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 3, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.SetActive(false);
    }
}
