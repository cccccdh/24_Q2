    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    public class Player : MonoBehaviour
    {
        // ������Ʈ ����
        private Rigidbody2D rb2D;
        private Animator ani;

        // ���� �Ŵ��� �� UI
        public GameManager gm;
        public Slider satietyBar;

        // �÷��̾� ���� ����
        private int satiety = 100;      // ������
        private bool isJumping = false; // ���� Ȯ��
        private bool isGrounded = false;// �� üũ
        private bool onCloud = false;   // ���� ���� �ִ��� Ȯ��

        // �̵� �� ���� �ӵ�
        public float speed;             // �̵��ӵ�
        public float jumpPower;         // ������

        void Start()
        {
            rb2D = GetComponent<Rigidbody2D>();
            ani = GetComponent<Animator>();
            gm = GameManager.instance;
            SetMaxSatiety();
        }

        void Update()
        {
            if ((transform.position.y < -7) || transform.position.y > 10)
                HandleDeath();

            if (gm.status == GameStatus.GameStart)
            {
                if (!onCloud)
                {
                    Move();
                    InputJump();
                }
                else
                    rb2D.velocity = Vector3.zero;
            }
            DecreaseSatiety();
            UpdateAnimation();
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
                collision.gameObject.SetActive(false);
                IncreaseSatiety(10);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Snowball")
                || collision.gameObject.CompareTag("Icicle"))
            {
                gm.SetStatus(GameStatus.GameOver);
                HandleDeath();
            }
        }

        // �ʱ� ������ ���� �Լ�
        void SetMaxSatiety()
        {
            satietyBar.maxValue = satiety;
            satietyBar.value = satiety;
        }

        // ������ ����
        void DecreaseSatiety()
        {
            // �̵��� �� ������ ����
            if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0.0f)
            {
                satietyBar.value -= 1.5f * Time.deltaTime;
            }

            // �� ����
            if (satietyBar.value >= 60)
            {
                satietyBar.fillRect.GetComponent<Image>().color = Color.green;
            }
            else if (satietyBar.value > 30 && satietyBar.value < 60)
            {
                satietyBar.fillRect.GetComponent<Image>().color = Color.yellow;
            }
            else
            {
                satietyBar.fillRect.GetComponent<Image>().color = Color.red;
            }
        }

        // ������ ����
        void IncreaseSatiety(int amount)
        {
            satietyBar.value += amount;
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

                if (rayHit.collider != null)
                {
                    isGrounded = rayHit.distance <= 0.6f;
                    isJumping = !isGrounded;

                    onCloud = rayHit.collider.CompareTag("Cloud");
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

        void HandleDeath()
        {
            gm.IncrementDeathCount();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
