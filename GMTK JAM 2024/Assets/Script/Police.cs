using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : MonoBehaviour
{
    public bool SeePlayer;
    public float SeeDistance;
    public float MoveSpeed;
    public LayerMask PlayerLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 lookDir = FindObjectOfType<Player>().transform.position - transform.position;
        //RaycastHit2D _hit = Physics2D.Raycast(transform.position, lookDir, SeeDistance, PlayerLayerMask);
        RaycastHit2D _hit = Physics2D.Raycast(transform.position, lookDir, SeeDistance);
        Debug.Log(_hit.collider);
    }
}
