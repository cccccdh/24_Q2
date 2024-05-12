using UnityEngine;
public enum ObstacleType
{
    Snowball,   // ������
    Icicle,     // ��帧
}

public class ObstacleTrigger : MonoBehaviour
{
    public ObstacleType type;
    public GameObject obj;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(type == ObstacleType.Snowball)
            {
                obj.GetComponent<Snowball>().AddForce();
            }
        }
    }
}
