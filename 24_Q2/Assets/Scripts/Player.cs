using UnityEngine;

public class Player : MonoBehaviour
{
    GameManager gameManager;
    Rigidbody2D rb2D;
    Animator ani;

    public bool isJumping = false;         // 점프 확인
    bool isGrounded = false;        // 땅 체크

    public float speed;             // 이동속도
    public float jumpPower;         // 점프력

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        CheckGround();
        if (isJumping) Jump();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fish"))
        {
            // 물고기 먹었을 때 물고기 사라지게하기
            collision.gameObject.SetActive(false);
            // 물고기 먹었을 때 허기 감소
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Snowball"))
        {
            // 눈덩이 닿았을 때 게임 종료 후 리스폰
            
        }
    }

    void Update()
    {
        Move();
        InputJump();
        UpdateAnimation();
    }

    // 이동 함수
    void Move()
    {
        float axisH = Input.GetAxis("Horizontal");

        // 좌우 방향 전환
        if (axisH > 0.0f)
            transform.localScale = new Vector2(0.7f, 0.7f);
        else if (axisH < 0.0f)
            transform.localScale = new Vector2(-0.7f, 0.7f);

        // 좌우 이동
        rb2D.velocity = new Vector2(speed * axisH, rb2D.velocity.y);
    }

    // 애니메이션 변경 함수
    void UpdateAnimation()
    {
        ani.SetBool("Run", Mathf.Abs(Input.GetAxis("Horizontal")) > 0.0f);
        ani.SetBool("Jump", !isGrounded);
    }

    // 땅 체크 함수
    void CheckGround()
    {
        if (rb2D.velocity.y < 0)
        {
            Debug.DrawRay(rb2D.position, Vector2.down, new Color(0, 1, 0));

            RaycastHit2D rayHit = Physics2D.Raycast(rb2D.position, Vector2.down, 0.6f, LayerMask.GetMask("Ground"));

            if (rayHit.collider != null && rayHit.distance <= 0.6f)
            {
                isJumping = false;
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        }
    }

    // 점프 입력 감지
    void InputJump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            isJumping = true;
            isGrounded = false;
        }
    }

    // 점프 함수
    void Jump()
    {
        rb2D.velocity = Vector2.up * jumpPower;
        isJumping = false;
    }
}
