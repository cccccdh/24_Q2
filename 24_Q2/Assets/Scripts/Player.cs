using UnityEngine;

public class Player : MonoBehaviour
{
    GameManager gameManager;
    Rigidbody2D rb2D;
    Animator ani;

    public bool isJumping = false;         // ���� Ȯ��
    bool isGrounded = false;        // �� üũ

    public float speed;             // �̵��ӵ�
    public float jumpPower;         // ������

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
            // ����� �Ծ��� �� ����� ��������ϱ�
            collision.gameObject.SetActive(false);
            // ����� �Ծ��� �� ��� ����
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Snowball"))
        {
            // ������ ����� �� ���� ���� �� ������
            
        }
    }

    void Update()
    {
        Move();
        InputJump();
        UpdateAnimation();
    }

    // �̵� �Լ�
    void Move()
    {
        float axisH = Input.GetAxis("Horizontal");

        // �¿� ���� ��ȯ
        if (axisH > 0.0f)
            transform.localScale = new Vector2(0.7f, 0.7f);
        else if (axisH < 0.0f)
            transform.localScale = new Vector2(-0.7f, 0.7f);

        // �¿� �̵�
        rb2D.velocity = new Vector2(speed * axisH, rb2D.velocity.y);
    }

    // �ִϸ��̼� ���� �Լ�
    void UpdateAnimation()
    {
        ani.SetBool("Run", Mathf.Abs(Input.GetAxis("Horizontal")) > 0.0f);
        ani.SetBool("Jump", !isGrounded);
    }

    // �� üũ �Լ�
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

    // ���� �Է� ����
    void InputJump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            isJumping = true;
            isGrounded = false;
        }
    }

    // ���� �Լ�
    void Jump()
    {
        rb2D.velocity = Vector2.up * jumpPower;
        isJumping = false;
    }
}
