using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerZombieController : MonoBehaviour {

    Rigidbody2D rb;

    private int LaterChance_count;
    private int State;
    private int Direction;

    public int Parameter_Speed;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();

        LaterChance_count = 0;
        State = 0;
        Direction = -1;
}
	
	// Update is called once per frame
	void Update () {
        if (LaterChance_count == 0)
        {
            State = 0;
            transform.Translate(new Vector2(Direction * (Parameter_Speed * 0.0075f), rb.velocity.y));
            if (new System.Random().Next(100) == 0)
            {
                Debug.Log("向き転換");
                Direction = -Direction;
            }

            if (Input.GetKeyDown("a"))  //プレイヤーがパンチ圏内か
            {
                Debug.Log("パンチ開始");
                LaterChance_count += 60;
                State = 1;
            }
            else if(Input.GetKeyDown("d")) //プレイヤーがダッシュ圏内か
            {
                if (new System.Random().Next(50) == 0) {
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
        Debug.Log("a");
    }

    void Dash()
    {
        Debug.Log("d");
        transform.Translate(new Vector2(Direction * (Parameter_Speed * 0.0075f), rb.velocity.y));
    }
}
