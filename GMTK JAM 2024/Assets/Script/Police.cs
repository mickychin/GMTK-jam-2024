using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Police : MonoBehaviour
{
    public bool SeePlayer;
    public float SeeDistance;
    public float speed;
    public float maxSpeed;
    public LayerMask PlayerLayerMask;
    Rigidbody2D rigibody2d;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        rigibody2d = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
    }

    private void FixedUpdate()
    {
        Vector2 lookDir = FindObjectOfType<Player>().transform.position - transform.position;
        //RaycastHit2D _hit = Physics2D.Raycast(transform.position, lookDir, SeeDistance, PlayerLayerMask);
        RaycastHit2D _hit = Physics2D.Raycast(transform.position, lookDir, SeeDistance);
        if(_hit.collider == null)
        {
            return;
        }

        Debug.Log(_hit.collider);
        if (_hit.collider.CompareTag("Player"))
        {
            if (player.transform.position.x > transform.position.x && rigibody2d.velocity.x < maxSpeed) // if player is on the right
            {
                //look right
                transform.localScale = new Vector2(transform.localScale.y, transform.localScale.y);
                //move right
                rigibody2d.velocity = new Vector2(rigibody2d.velocity.x + speed, rigibody2d.velocity.y);
            }

            if (player.transform.position.x < transform.position.x && rigibody2d.velocity.x > -maxSpeed) // if player is on the left
            {
                //look left
                transform.localScale = new Vector2(-transform.localScale.y, transform.localScale.y);
                //move left
                rigibody2d.velocity = new Vector2(rigibody2d.velocity.x - speed, rigibody2d.velocity.y);
            }
        }
    }
}
