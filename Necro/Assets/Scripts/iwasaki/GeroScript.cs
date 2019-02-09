using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeroScript : MonoBehaviour {
    Rigidbody2D rb;

    public bool LorR;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        
        if (LorR == false)
        {
            rb.AddForce(Vector2.left * 250f);
        }
        else
        {
            rb.AddForce(Vector2.right * 250f);
        }
        rb.AddForce(Vector2.up * 250f);
    }

    void OnCollisionEnter2D(Collision2D hit)
    {
        if (hit.gameObject.tag != TagName.zombie && hit.gameObject.tag != TagName.player)
        {
            Destroy(gameObject);
        }

    }
}
