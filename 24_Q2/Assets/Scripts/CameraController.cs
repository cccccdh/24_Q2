using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;    // Ÿ�� ��ġ
    public Vector3 offset;      // �ʱ� �� 

    void Update()
    {
        transform.position = new Vector3(target.position.x, 0, 0)  + offset;
    }
}
