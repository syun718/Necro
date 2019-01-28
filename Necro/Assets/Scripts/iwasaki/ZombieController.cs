using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    Rigidbody2D rb;

    public GameObject GeroZomie_Gero;
    public GameObject Player;

    public enum ZombieType
    {
        GeroZombie,
        DogZombie,
        PowerZombie,
        BirdZombie
    }

    public ZombieType Type;

    private int LaterChance_count;  //攻撃後の後隙時間カウンタ
    private int State;  //0で待機
                        //1で攻撃１
                        //2で攻撃２

    public int Parameter_Speed; //待機中の移動スピード
    public float Attack1_Range; //攻撃射程距離
    public float Attack2_Range;
    //public float Parameter_DashSpeed;

    void Start()
    {
        GameObject Player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();

        LaterChance_count = 0;
        State = 0;

        switch (Type)
        {
            case ZombieType.GeroZombie:
                Attack1_Range = 1;
                Attack2_Range = 1;
                break;
            case ZombieType.DogZombie:
                Attack1_Range = 1;
                Attack2_Range = 1;
                break;
            case ZombieType.PowerZombie:
                Attack1_Range = 1;
                Attack2_Range = 1;
                break;
            case ZombieType.BirdZombie:
                Attack1_Range = 1;
                Attack2_Range = 1;
                break;
        }   //後隙時間の設定
    }

    // Update is called once per frame
    void Update()
    {
        if (LaterChance_count == 0)
        {
            State = 0;
            GameObject Player = GameObject.FindGameObjectWithTag("Player");

            transform.Translate(new Vector2((Parameter_Speed * 0.05f), rb.velocity.y)); //移動

            if (Input.GetKeyDown("a"))  //プレイヤーが攻撃１の射程圏内か
            {
                Debug.Log("攻撃１開始");

                switch (Type) {
                    case ZombieType.GeroZombie:
                        LaterChance_count += 60;
                        break;
                    case ZombieType.DogZombie:
                        LaterChance_count += 60;
                        break;
                    case ZombieType.PowerZombie:
                        LaterChance_count += 60;
                        break;
                    case ZombieType.BirdZombie:
                        LaterChance_count += 60;
                        break;
                }   //後隙時間の設定
                State = 1;
            }
            else if (Input.GetKeyDown("d")) //プレイヤーが攻撃２の射程圏内か
            {
                Debug.Log("攻撃２開始");

                switch (Type)
                {
                    case ZombieType.GeroZombie:
                        LaterChance_count += 60;
                        break;
                    case ZombieType.DogZombie:
                        LaterChance_count += 60;
                        break;
                    case ZombieType.PowerZombie:
                        LaterChance_count += 60;
                        break;
                    case ZombieType.BirdZombie:
                        LaterChance_count += 60;
                        break;
                }   //後隙時間の設定
                State = 2;
            }
        }else
        {
            switch (State)
            {
                case 1:
                    Attack1();
                    break;

                case 2:
                    Attack2();
                    break;
            }//攻撃関数呼び出し

            LaterChance_count--;
        }
    }

    void Attack1()
    {
        switch (Type)
        {
            case ZombieType.GeroZombie:
                if (LaterChance_count == 60)
                {

                }
                else if (LaterChance_count == 50)
                {
                    Instantiate(GeroZomie_Gero, transform.position, Quaternion.identity);
                }
                break;

            case ZombieType.DogZombie:
                break;

            case ZombieType.PowerZombie:
                break;

            case ZombieType.BirdZombie:
                break;

        }   //攻撃１のタイムテーブル設定
        //Debug.Log("Attack1:"+ LaterChance_count);
    }

    void Attack2()
    {
        switch (Type)
        {
            case ZombieType.GeroZombie:
                if (LaterChance_count == 60)
                {

                }
                else if (LaterChance_count == 50)
                {
                    Instantiate(GeroZomie_Gero, transform.position, Quaternion.identity);
                }
                break;

            case ZombieType.DogZombie:
                break;

            case ZombieType.PowerZombie:
                break;

            case ZombieType.BirdZombie:
                break;

        }   //攻撃２のタイムテーブル設定
        //Debug.Log("Attack2:"+ LaterChance_count);
    }
}
