using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float TimeToDestroy;
    public int GroundLayern;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Despawn());
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(TimeToDestroy);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.layer);
        if (collision == null)
        {
            //Debug.Log("HOW");
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            //kill player
            collision.gameObject.GetComponent<Player>().Death();
            Destroy(gameObject);
        } 
        else if(collision.gameObject.layer == GroundLayern)
        {
            Destroy(gameObject);
        }
    }
}
