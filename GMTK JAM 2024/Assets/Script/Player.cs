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
    public Transform groundCheck1;
    public Transform groundCheck2;
    public LayerMask whatIsGround;
    public float CayoteTime;
    private bool IsGrounded;
    private bool IsCayote;
    private bool Cayoteing;
    public float JumpBufferTime;
    private bool IsJumpBuffer;

    [Header("Wall Run")]
    public bool Grappling1;
    public bool Grappling2;
    public Transform DetectPos;
    public Transform DetectLeftPos;
    public bool IsOnWall;
    public bool IsOnLeftWall;
    public float DetectWallRange;
    public float wallSpeed;
    public float MaxWallSpeed;

    public Animator animator;

    [Header("Death")]
    public float RespawnTime;
    public GameObject DeathParticle;
    private bool IsDead;
    private Transform LastCheckPoint;
    public GameObject JumpParticle;
    public Transform JumpParticleSpawns;

    // Start is called before the first frame update
    void Start()
    {
        Cayoteing = false;
        rigibody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (IsDead)
        {
            return;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            IsJumpBuffer = true;
            StopCoroutine(JumpBufferTimer());
            StartCoroutine(JumpBufferTimer());
        }

        if (IsJumpBuffer && (IsGrounded || IsCayote))
        {
            //animator.SetBool("Falling", true);
            
            Instantiate(JumpParticle, JumpParticleSpawns.position, Quaternion.identity);
            animator.SetTrigger("Jump");
            rigibody2D.velocity = new Vector2(rigibody2D.velocity.x, rigibody2D.velocity.y) + Vector2.up * JumpForce;
            IsJumpBuffer = false;
            IsCayote = false;
            Cayoteing = false;
            StopCoroutine(JumpBufferTimer());
            StopCoroutine(CayoteTimer());
        }
    }

    private void FixedUpdate()
    {
        if (IsDead)
        {
            return;
        }
        IsOnWall = Physics2D.OverlapCircle(DetectPos.position, DetectWallRange, whatIsGround);
        IsOnLeftWall = Physics2D.OverlapCircle(DetectLeftPos.position, DetectWallRange, whatIsGround);
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

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            animator.SetBool("Walking", true);
            if(rigibody2D.velocity.y == 0)
            {
                animator.speed = Mathf.Abs(rigibody2D.velocity.x / 10);
            }
            else
            {
                animator.speed = 1;
            }
        }
        else
        {
            animator.SetBool("Walking", false);
            animator.speed = 1;
        }

        moveInput = Input.GetAxis("Horizontal");
        if(rigibody2D.velocity.x < MaxSpeed && moveInput > 0)
        {
            // move right
            animator.gameObject.transform.localScale = new Vector2(1,1);
            rigibody2D.velocity = new Vector2(rigibody2D.velocity.x + moveInput * speed, rigibody2D.velocity.y);
        }
        if (rigibody2D.velocity.x > -MaxSpeed && moveInput < 0)
        {
            //move left
            animator.gameObject.transform.localScale = new Vector2(-1, 1);
            rigibody2D.velocity = new Vector2(rigibody2D.velocity.x + moveInput * speed, rigibody2D.velocity.y);
        }
        if(moveInput == 0)
        {
            //friction
            rigibody2D.velocity = new Vector2(rigibody2D.velocity.x * friction, rigibody2D.velocity.y);
        }
        else if (moveInput > 0 && rigibody2D.velocity.x < 0)
        {
            //friction
            rigibody2D.velocity = new Vector2(rigibody2D.velocity.x * friction, rigibody2D.velocity.y);
        }
        else if (moveInput < 0 && rigibody2D.velocity.x > 0)
        {
            //friction
            rigibody2D.velocity = new Vector2(rigibody2D.velocity.x * friction, rigibody2D.velocity.y);
        }

        if(IsOnWall)
        {
            //wall run
            if(moveInput > 0 && rigibody2D.velocity.y < MaxSpeed)
            {
                rigibody2D.velocity = new Vector2(rigibody2D.velocity.x, rigibody2D.velocity.y + wallSpeed);
            }
        }
        else if (IsOnLeftWall)
        {
            //wall run
            if (moveInput < 0 && rigibody2D.velocity.y < MaxSpeed)
            {
                rigibody2D.velocity = new Vector2(rigibody2D.velocity.x, rigibody2D.velocity.y + wallSpeed);
            }
        }

        if(rigibody2D.velocity.y < 0)
        {
            //falling
            animator.SetBool("Falling", true);
            if (IsGrounded)
            {
                GetComponent<AudioSource>().Play();
                Instantiate(JumpParticle, JumpParticleSpawns.position, Quaternion.identity);
            }

        }
        else
        {
            animator.SetBool("Falling", false);
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

    public void Death()
    {
        //Debug.Log("death");
        StartCoroutine(DeathAndRespawn());
    }

    IEnumerator DeathAndRespawn()
    {
        //kill player
        //run death anim
        //sfx
        //Debug.Log("Deathandres");
        IsDead = true;
        GameObject spawnedDeathParticle = Instantiate(DeathParticle, new Vector3(transform.position .x, transform.position.y, transform.position.z), Quaternion.identity);
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(RespawnTime);
        GetComponent<BoxCollider2D>().enabled = true;
        IsDead = false;
        transform.position = LastCheckPoint.position;
        Destroy(spawnedDeathParticle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Checkpoints"))
        {
            LastCheckPoint = collision.transform;
        }
    }
}
