using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb2D;
    Animator ani;

    float axisH;

    bool isJump = false;            // 점프 확인

    public float speed;             // 이동속도
    public float jumpPower;         // 점프력

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }


    void FixedUpdate()
    {
        if(rb2D.velocity.y < 0)
        {
            Debug.DrawRay(rb2D.position, Vector2.down, new Color(0, 1, 0));

            RaycastHit2D rayHit = Physics2D.Raycast(rb2D.position, Vector2.down, 1, LayerMask.GetMask("Ground"));

            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.6f)
                {
                    isJump = false;
                    ani.SetBool("Jump", false);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("SnowBall"))
        {

        }
    }

    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isJump)
        {
            isJump = true;
            ani.SetBool("Jump", true);
            Jump();
        }
    }

    // 이동 함수
    void Move()
    {
        axisH = Input.GetAxis("Horizontal");

        if (axisH > 0.0f)
            transform.localScale = new Vector2(0.7f, 0.7f);
        else if (axisH < 0.0f)
            transform.localScale = new Vector2(-0.7f, 0.7f);

        rb2D.velocity = new Vector2(speed * axisH, rb2D.velocity.y);
        ani.SetBool("Run", true);
    }

    // 점프 함수
    void Jump()
    {
        if (isJump)
        {
            rb2D.velocity = Vector2.up * jumpPower;
        }
    }
}
