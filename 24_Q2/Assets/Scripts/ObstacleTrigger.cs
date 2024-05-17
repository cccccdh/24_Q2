using UnityEngine;
public enum ObstacleType
{
    Snowball,   // ´«µ¢ÀÌ
    Icicle,     // °íµå¸§
    cloud       // ±¸¸§
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
            else if(type == ObstacleType.Icicle)
            {
                obj.GetComponent<Icicle>().UseGravity();
            }
            else if(type == ObstacleType.cloud)
            {
                obj.GetComponent<Cloud>().MoveUp();
            }
        }
    }
}
