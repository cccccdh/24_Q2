using TreeEditor;
using UnityEngine;

public class Crack : MonoBehaviour
{
    float time = 0;
    bool isEnter = false;

    void Update()
    {
        if (isEnter)
        {
            time += Time.deltaTime;
        }else
        {
            time = 0;
        }
        if(time > 0.2f)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isEnter = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isEnter = false;
        }
    }
}
