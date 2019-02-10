using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    Rigidbody2D rb;

    PlayerAnimations m_playerAnimations;

    public GameObject GeroZomie_L_Gero;
    public GameObject GeroZomie_R_Gero;
    public GameObject Attack_Area;

    public enum ZombieType
    {
        GeroZombie,
        DogZombie,
        PowerZombie,
        BirdZombie
    }

    public ZombieType Type;

    public int LaterChance_count;  //攻撃後の後隙時間カウンタ
    public int State;  //0で移動可能
                        //1で攻撃１
                        //2で攻撃２

    public float Parameter_Speed; //待機中の移動スピード
    public bool Range_Flag1;
    public bool Range_Flag2;
    public float Parameter_DashSpeed;   //一部キャラのダッシュスピード
    public int Rand_Percent;    //攻撃する確率
    public bool Hijack_Frag;    //乗っ取られているか否か

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        m_playerAnimations = GetComponent<PlayerAnimations>();

        LaterChance_count = 0;
        State = 0;
        Range_Flag1 = false;
        Range_Flag2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Hijack_Frag == false)
        {
            if (State == 0)
            {
                transform.Translate(new Vector2((Parameter_Speed * 0.05f), rb.velocity.y)); //移動
                m_playerAnimations.MoveAnimation(1.0f); m_playerAnimations.MoveAnimation(1.0f);
            }
            if (LaterChance_count == 0)
            {
                State = 0;

                if (Range_Flag1 == true && Random.Range(0, Rand_Percent) == 0)  //プレイヤーが攻撃１の射程圏内か
                {
                    AttackCoal1();
                }
                else if (Range_Flag2 == true && Random.Range(0, Rand_Percent) == 0) //プレイヤーが攻撃２の射程圏内か
                {
                    AttackCoal2();
                }
            }
            else
            {
                    switch (State)
                {
                    case 1:
                        Attack1();
                        break;

                    case 2:
                        Attack2();
                        break;

                    default:
                        break;
                }//攻撃関数呼び出し
                LaterChance_count--;
            }
        }
        else {
            if (LaterChance_count == 0)
            {
                State = 0;
            }
            switch (State)
            {
                case 1:
                    Attack1();
                    LaterChance_count--;
                    break;

                case 2:
                    Attack2();
                    LaterChance_count--;
                    break;

                default:
                    break;
            }//攻撃関数呼び出し
            
        }
    }

    public void AttackCoal1()
    {
        Debug.Log("攻撃１開始");

        switch (Type)
        {
            case ZombieType.GeroZombie:
                LaterChance_count = 60;
                break;
            case ZombieType.DogZombie:
                LaterChance_count += 180;
                break;
            case ZombieType.PowerZombie:
                LaterChance_count += 120;
                break;
            case ZombieType.BirdZombie:
                LaterChance_count += 300;
                break;
        }   //後隙時間の設定
        State = 1;
    }

    public void AttackCoal2()
    {

        Debug.Log("攻撃２開始");

        switch (Type)
        {
            case ZombieType.GeroZombie:
                LaterChance_count += 0;
                break;
            case ZombieType.DogZombie:
                LaterChance_count += 0;
                break;
            case ZombieType.PowerZombie:
                LaterChance_count += 0;
                break;
            case ZombieType.BirdZombie:
                LaterChance_count += 1;
                break;
        }   //後隙時間の設定
        State = 2;
    }

    void Attack1()
    {
        switch (Type)
        {
            case ZombieType.GeroZombie:
                if (LaterChance_count == 60)
                {
                    m_playerAnimations.GeroAnimation(); m_playerAnimations.GeroAnimation();
                }
                else if (LaterChance_count == 50)
                {
                    if (Hijack_Frag)
                    {
                        Instantiate(GeroZomie_R_Gero, transform.position, Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(GeroZomie_L_Gero, transform.position, Quaternion.identity);
                    }

                }
                break;

            case ZombieType.DogZombie:
                if (LaterChance_count == 180)
                {
                    Parameter_Speed = 0;
                }
                else if (LaterChance_count == 1)
                {
                    Parameter_Speed = 1;
                }
                else if (LaterChance_count <= 171)
                {
                    if (Parameter_Speed <= Parameter_DashSpeed * 1.5f)
                    {
                        Parameter_Speed += 0.05f;
                    }
                    transform.Translate(new Vector2(Parameter_Speed * 0.05f, rb.velocity.y)); //移動
                }
                break;

            case ZombieType.PowerZombie:
                if (LaterChance_count == 120)
                {
                    Parameter_Speed = 0;
                    m_playerAnimations.TackleAnimation();
                }
                else if (LaterChance_count <= 11)
                {
                    Parameter_Speed = 0.5f;
                }
                else if (LaterChance_count <= 111)
                {
                    if (Parameter_Speed <= Parameter_DashSpeed)
                    {
                        Parameter_Speed += 0.1f;
                    }
                    transform.Translate(new Vector2(Parameter_Speed * 0.05f, rb.velocity.y)); //移動
                    m_playerAnimations.TackleAnimation();
                }
                break;

            case ZombieType.BirdZombie:
                if (LaterChance_count == 291)
                {
                    rb.AddForce(Vector2.up * 400.0f);
                }else if (LaterChance_count == 180)
                {
                    rb.AddForce(Vector2.left * 250.0f);
                    rb.AddForce(Vector2.down * 100.0f);
                }
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
                    //Instantiate(GeroZomie_Gero, transform.position, Quaternion.identity);
                }
                break;

            case ZombieType.DogZombie:
                break;

            case ZombieType.PowerZombie:
                break;

            case ZombieType.BirdZombie:
                Debug.Log("hri");
                rb.AddForce(Vector2.up * 10f);
                break;

        }   //攻撃２のタイムテーブル設定
        //Debug.Log("Attack2:"+ LaterChance_count);
    }

    public void Fri()
    {
        Debug.Log("tonda");
        rb.AddForce(Vector2.up * 1000f);
    }
}
