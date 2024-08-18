using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    [Header("Movement")]
    public float MinDistance;
    public float Distance;
    public float speed;
    public float MaxSpeed;

    [Header("Shoot")]
    public float FireSpeed;
    public GameObject bullet;
    public float bulletSpeed;
    public Transform FirePoint;
    public GameObject Shooter;
    public Transform ShooterPos;

    private Player player;
    private Rigidbody2D rigibody2d;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        rigibody2d = GetComponent<Rigidbody2D>();
        StartCoroutine(Shooting());
    }

    // Update is called once per frame
    void Update()
    {
        Distance = Mathf.Abs(transform.position.x - player.transform.position.x) + Mathf.Abs(transform.position.y - player.transform.position.y);
    }

    private void FixedUpdate()
    {
        if (player.transform.position.x > transform.position.x && Distance > 5 && rigibody2d.velocity.x < MaxSpeed) // if player is on the right
        {
            //look right
            transform.localScale = new Vector2(transform.localScale.y, transform.localScale.y);
            //move right
            rigibody2d.velocity = new Vector2(rigibody2d.velocity.x + speed, rigibody2d.velocity.y);
        }

        if (player.transform.position.x < transform.position.x && Distance > 5 && rigibody2d.velocity.x > -MaxSpeed) // if player is on the left
        {
            //look left
            transform.localScale = new Vector2(-transform.localScale.y, transform.localScale.y);
            //move left
            rigibody2d.velocity = new Vector2(rigibody2d.velocity.x - speed, rigibody2d.velocity.y);
        }

        if (player.transform.position.y > transform.position.y && Distance > 5 && rigibody2d.velocity.y < MaxSpeed) // if player is on the top
        {
            //move up
            rigibody2d.velocity = new Vector2(rigibody2d.velocity.x, rigibody2d.velocity.y + speed);
        }

        if (player.transform.position.y < transform.position.y && Distance > 5 && rigibody2d.velocity.y > -MaxSpeed) // if player is on the bottom
        {
            //move down
            rigibody2d.velocity = new Vector2(rigibody2d.velocity.x, rigibody2d.velocity.y - speed);
        }

        Vector2 lookDir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;
        Shooter.GetComponent<Rigidbody2D>().rotation = angle;
        Shooter.transform.position = ShooterPos.position;
    }

    IEnumerator Shooting()
    {
        Shoot();
        yield return new WaitForSeconds(FireSpeed);
        StartCoroutine(Shooting());
    }

    void Shoot()
    {
        GameObject shotedBullet = Instantiate(bullet, FirePoint.position, FirePoint.rotation);
        //shotedBullet.transform.Rotate(0,0,90);
        shotedBullet.GetComponent<Rigidbody2D>().AddForce(FirePoint.up * bulletSpeed, ForceMode2D.Impulse);
    }
}
