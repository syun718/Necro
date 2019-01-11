using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdZombieController : MonoBehaviour
{
    Rigidbody2D rb;

    private int LaterChance_count;
    private int State;

    public int Parameter_Speed;
    public float Parameter_DashSpeed;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        LaterChance_count = 0;
        State = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(-Parameter_Speed * 0.05f, rb.velocity.y));
        if (LaterChance_count == 0)
        {
            State = 0;

            if ((new System.Random().Next(200) == 0) || (transform.position.x >= 7 || transform.position.x <= -7))
            {
                Debug.Log("向き転換");
                transform.Rotate(0, 180, 0);
            }

            if (Input.GetKeyDown("a"))  //プレイヤーが攻撃圏内か
            {
                Debug.Log("攻撃開始");

                

                LaterChance_count += 60;
                State = 1;
            }
            else if (Input.GetKeyDown("d")) //プレイヤーがダッシュ圏内か
            {
                if (new System.Random().Next(50) == 0)
                {
                    Debug.Log("ダッシュ開始");
                    LaterChance_count += 120;
                    State = 2;
                }
            }
        }
        else
        {
            switch (State)
            {
                case 1:
                    Attack();
                    break;

                case 2:
                    Dash();
                    break;
            }

            LaterChance_count--;
        }
    }

    void Attack()
    {
        if (LaterChance_count >= 50)
        {

        }
        else if (LaterChance_count >= 20)
        {
            //攻撃処理
        }
        Debug.Log("a");
    }

    void Dash()
    {
        Debug.Log("d");
        if (LaterChance_count >= 100)
        {

        }
        else if (LaterChance_count >= 20)
        {
            transform.Translate(new Vector2(Parameter_Speed * Parameter_DashSpeed * 0.0075f, rb.velocity.y));
        }
        else if (LaterChance_count >= 0)
        {

        }
    }
}
