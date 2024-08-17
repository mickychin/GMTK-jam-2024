using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Run")]
    public float speed;
    public float JumpForce;
    private float moveInput;

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
    private bool JustJumped;

    // Start is called before the first frame update
    void Start()
    {
        Cayoteing = false;
        rigibody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            IsJumpBuffer = true;
            StopCoroutine(JumpBufferTimer());
            StartCoroutine(JumpBufferTimer());
        }

        if (IsJumpBuffer && (IsGrounded || IsCayote))
        {
            rigibody2D.velocity = Vector2.up * JumpForce;
            IsJumpBuffer = false;
            IsCayote = false;
            Cayoteing = false;
            JustJumped = true;
            StopCoroutine(JumpBufferTimer());
            StopCoroutine(CayoteTimer());
        }

        if (Input.GetKeyUp(KeyCode.W) && rigibody2D.velocity.y > 0)
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
        rigibody2D.velocity = new Vector2(moveInput * speed, rigibody2D.velocity.y);
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
