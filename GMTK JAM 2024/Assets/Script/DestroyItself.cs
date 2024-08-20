using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyItself : MonoBehaviour
{
    public float TimeBe4Destroy;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyInSeconds());
    }

    IEnumerator DestroyInSeconds()
    {
        yield return new WaitForSeconds(TimeBe4Destroy);
        Destroy(gameObject);
    }
}
