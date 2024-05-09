using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;    // 타겟 위치
    public Vector3 offset;      // 초기 값 

    void Update()
    {
        transform.position = new Vector3(target.position.x, 0, 0)  + offset;
    }
}
