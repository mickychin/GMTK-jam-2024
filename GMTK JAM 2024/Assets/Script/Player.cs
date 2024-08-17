using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float JumpForce;
    private float moveInput;

    private Rigidbody2D rigibody2D;

    private bool IsGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    // Start is called before the first frame update
    void Start()
    {
        rigibody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            rigibody2D.velocity = Vector2.up * JumpForce;
        }
    }

    private void FixedUpdate()
    {
        IsGrounded = Physics2D.OverlapCircle(groundCheck.position, whatIsGround);

        moveInput = Input.GetAxis("Horizontal");
        rigibody2D.velocity = new Vector2(moveInput * speed, rigibody2D.velocity.y);
    }


}
