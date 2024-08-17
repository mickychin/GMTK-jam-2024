using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Run")]
    public float speed;
    public float MaxSpeed;
    public float JumpForce;
    private float moveInput;
    public float friction;

    private Rigidbody2D rigibody2D;

    [Header("Jump")]
    private bool IsGrounded;
    public Transform groundCheck1;
    public Transform groundCheck2;
    public LayerMask whatIsGround;
    public float CayoteTime;
    private bool IsCayote;
    private bool Cayoteing;
    public float JumpBufferTime;
    private bool IsJumpBuffer;
    public bool JustJumped;

    // Start is called before the first frame update
    void Start()
    {
        Cayoteing = false;
        rigibody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            IsJumpBuffer = true;
            StopCoroutine(JumpBufferTimer());
            StartCoroutine(JumpBufferTimer());
        }

        if (IsJumpBuffer && (IsGrounded || IsCayote))
        {
            rigibody2D.velocity = new Vector2(rigibody2D.velocity.x, rigibody2D.velocity.y) + Vector2.up * JumpForce;
            IsJumpBuffer = false;
            IsCayote = false;
            Cayoteing = false;
            JustJumped = true;
            StopCoroutine(JumpBufferTimer());
            StopCoroutine(CayoteTimer());
        }

        if ((Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.Space)) && rigibody2D.velocity.y > 0 && JustJumped)
        {
            JustJumped = false;
            rigibody2D.velocity = new Vector2(rigibody2D.velocity.x, 0);
        }
    }

    private void FixedUpdate()
    {
        IsGrounded = Physics2D.OverlapArea(groundCheck1.position, groundCheck2.position, whatIsGround);
        if (IsGrounded)
        {
            IsCayote = true;
        }

        if(!IsGrounded && IsCayote && Cayoteing == false)
        {
            Cayoteing = true;
            StartCoroutine(CayoteTimer());
        }

        moveInput = Input.GetAxis("Horizontal");
        if(rigibody2D.velocity.x < MaxSpeed && moveInput > 0)
        {
            rigibody2D.velocity = new Vector2(rigibody2D.velocity.x + moveInput * speed, rigibody2D.velocity.y);
        }
        if (rigibody2D.velocity.x > -MaxSpeed && moveInput < 0)
        {
            rigibody2D.velocity = new Vector2(rigibody2D.velocity.x + moveInput * speed, rigibody2D.velocity.y);
        }
        if (moveInput == 0)
        {
            rigibody2D.velocity = new Vector2(rigibody2D.velocity.x * friction, rigibody2D.velocity.y);
        }
    }

    IEnumerator CayoteTimer()
    {
        yield return new WaitForSeconds(CayoteTime);
        IsCayote = false;
        Cayoteing = false;
    }

    IEnumerator JumpBufferTimer()
    {
        yield return new WaitForSeconds(JumpBufferTime);
        IsJumpBuffer = false;
    }
}
