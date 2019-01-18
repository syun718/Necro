using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gerozo : MonoBehaviour {
    Rigidbody2D m_rigid2D;

    private void Start()
    {
        m_rigid2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
       
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("当たった");
        if (col.gameObject.tag == "Zombie")
        {
            Debug.Log("いわっさき");
            m_rigid2D.AddForce(Vector2.up * 10000);
            m_rigid2D.AddForce(Vector2.right * 10000);
        }
    }
}
