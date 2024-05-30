using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [SerializeField] private float jumpForce;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private BoxCollider2D normalCollider;
    [SerializeField] private BoxCollider2D slideCollider;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;



    private Rigidbody2D rb;
    private Animator animator;
    private BoxCollider2D currentCollider;
    private bool isGrounded;
    private bool isDashing = false;
    public State state;
    

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();    
    }
    void Start()
    {
        state = State.Playing;
        ParallaxManager.instance.IsParallaxActive = true;
        currentCollider = normalCollider;
        slideCollider.enabled = false;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Player"), false);

    }

    void Update()
    {
        switch (state)
        {
            case State.Standby:
                break;
            case State.Playing:
                GroundCheck();
                Jump();
                Attack();
                Dash();
                Slide();
                AnimationController();
                break;
            case State.GameOver:
                OnPlayerDeath();
                break;
        }
    }

    private void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up *  jumpForce, ForceMode2D.Impulse);
        }
    }

    private void AnimationController()
    {
        animator.SetFloat("vertical_velocity", rb.velocity.y);
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            animator.SetTrigger("attack");
        }
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.K) && !isDashing)
        {
            isDashing = true;
            animator.SetTrigger("dash");
        }
    }
    IEnumerator DashCoroutine()
    {
        // Set tốc độ cho timescale
        Time.timeScale = dashSpeed;

        // chờ thời gian dash
        yield return new WaitForSeconds(dashDuration);

        // Reset timescale về ban đầu
        Time.timeScale = 1f;

        isDashing = false;
    }

    public void StartDashEffect()
    {
        StartCoroutine(DashCoroutine());
    }

    private void Slide()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            animator.SetTrigger("slide");
            StartSliding();
        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            StopSliding();
        }
    }
    private void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));
        isGrounded = hit.collider != null;
    }

    void StartSliding()
    {
        SwitchToSlideCollider();
        
    }

    private void StopSliding()
    {
        SwitchToNormalCollider();
       
    }

    private void SwitchToNormalCollider()
    {
        currentCollider.enabled = false;
        normalCollider.enabled = true;
        currentCollider = normalCollider;
    }

    private void SwitchToSlideCollider()
    {
        currentCollider.enabled = false;
        slideCollider.enabled = true;
        currentCollider = slideCollider;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Enemy"))
        {
            animator.SetTrigger("death");
            state = State.GameOver;
        }
    }

    private void OnPlayerDeath()
    {
        // Tắt va chạm giữa lớp player và lớp enemy
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Player"), true);

        //Tắt parallax
        ParallaxManager.instance.IsParallaxActive = false;

        //lưu điểm cao nhất
        ScoreManager.Instance.SaveHighScore();

        MainMenu.Instance.MenuGameOver.SetActive(true);

    }

    public enum State
    {
        Standby,
        Playing,
        GameOver
    }
}
