using System.Collections;
using UnityEngine;

public class MoveGround : MonoBehaviour
{
    Vector3 TargetPos;
    public float speed = 1.0f; // 이동 속도

    bool isMovingLeft = false;
    bool isMovingRight = false;

    private void Start()
    {
        
    }

    public void MovingL()
    {
        if (!isMovingLeft)
        {
            StartCoroutine(MoveToPosition_LEFT(speed));
        }
    }

    private IEnumerator MoveToPosition_LEFT(float speed)
    {
        isMovingLeft = true;

        float time = 0f;

        Vector3 initialPosition = transform.position;

        TargetPos = new Vector3(transform.position.x + 2, transform.position.y, transform.position.z);

        while (time < 1f)
        {
            time += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(initialPosition, TargetPos, time);
            yield return null;
        }

        transform.position = TargetPos; 
        isMovingLeft = false;
    }

    public void MovingR()
    {
        if (!isMovingLeft)
        {
            StartCoroutine(MoveToPosition_RIGHT(speed));
        }
    }

    private IEnumerator MoveToPosition_RIGHT(float speed)
    {
        isMovingLeft = true;
        float time = 0f;
        Vector3 initialPosition = transform.position;

        TargetPos = new Vector3(transform.position.x - 3, transform.position.y, transform.position.z);

        while (time < 1f)
        {
            time += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(initialPosition, TargetPos, time);
            yield return null;
        }

        transform.position = TargetPos; // 정확한 위치로 이동
        isMovingLeft = false;
    }
}
