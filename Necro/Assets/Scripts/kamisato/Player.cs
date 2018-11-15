using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    Rigidbody2D m_rigidBody;
    string xAxisName = "Horizontal";
    float moveSpeed = 5f;

    private UiController uiController;

    // Use this for initialization
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
        uiController = GameObject.Find("UiController").GetComponent<UiController>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        Vector3 move = Vector3.zero;
        move.x = Input.GetAxisRaw(xAxisName);
        m_rigidBody.velocity = move * moveSpeed;
    }

     void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Zombie1")
        {
            //uiController.ChangePlayerIcom();
        }
    }
}
