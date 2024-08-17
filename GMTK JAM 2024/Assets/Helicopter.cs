using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    public float MinDistance;
    public float Distance;
    public float speed;
    public float MaxSpeed;
    private Player player;
    private Rigidbody2D rigibody2d;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        rigibody2d = GetComponent<Rigidbody2D>();
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
            //move right
            rigibody2d.velocity = new Vector2(rigibody2d.velocity.x + speed, rigibody2d.velocity.y);
        }

        if (player.transform.position.x < transform.position.x && Distance > 5 && rigibody2d.velocity.x > -MaxSpeed) // if player is on the left
        {
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
    }
}
