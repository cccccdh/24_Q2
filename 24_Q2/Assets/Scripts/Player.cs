    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    public class Player : MonoBehaviour
    {
        // 컴포넌트 참조
        private Rigidbody2D rb2D;
        private Animator ani;

        // 게임 매니저 및 UI
        public GameManager gm;
        public Slider satietyBar;

        // 플레이어 상태 변수
        private int satiety = 100;      // 포만감
        private bool isJumping = false; // 점프 확인
        private bool isGrounded = false;// 땅 체크
        private bool onCloud = false;   // 구름 위에 있는지 확인

        // 이동 및 점프 속도
        public float speed;             // 이동속도
        public float jumpPower;         // 점프력

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

        // 초기 포만감 설정 함수
        void SetMaxSatiety()
        {
            satietyBar.maxValue = satiety;
            satietyBar.value = satiety;
        }

        // 포만감 감소
        void DecreaseSatiety()
        {
            // 이동할 때 포만감 감소
            if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0.0f)
            {
                satietyBar.value -= 1.5f * Time.deltaTime;
            }

            // 색 변경
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

        // 포만감 증가
        void IncreaseSatiety(int amount)
        {
            satietyBar.value += amount;
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

                if (rayHit.collider != null)
                {
                    isGrounded = rayHit.distance <= 0.6f;
                    isJumping = !isGrounded;

                    onCloud = rayHit.collider.CompareTag("Cloud");
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

        void HandleDeath()
        {
            gm.IncrementDeathCount();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
