using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    //最も近いオブジェクト
    public GameObject nearObj;
    public GameObject m_mainCamera;

    Rigidbody2D m_rigid2D;
    PlayerInput m_playerInput;
    ChangePlayer m_changePlayer;
    public float m_Speed;
    public float m_flap;
    public float m_jobTime;
    public float m_DestroyTime;

    public int m_jobNum;
    public int m_zombiehit;

    bool m_jump = false;

    private UiController uiController;

    // Use this for initialization
    void Start () {
        PlayerData.Instance.playerHp = 0;
        m_Speed = 5f;
        m_flap = 1500f;
        m_DestroyTime = 5;
        Status();
        PlayerData.Instance.SetJobTime = PlayerData.Instance.jobTime;

        //最も近かったオブジェクトを取得
        nearObj = serchTag(gameObject, "Player");
        m_playerInput = GetComponent<PlayerInput>();
        m_rigid2D = GetComponent<Rigidbody2D>();
        uiController = GameObject.Find("UiController").GetComponent<UiController>();
        m_changePlayer = GameObject.Find("PlayerManager").GetComponent<ChangePlayer>();
        Status();
        PlayerData.Instance.SetJobTime = PlayerData.Instance.jobTime;
        //最も近かったオブジェクトを取得
        nearObj = serchTag(gameObject, "Player");
        switch (m_jobNum)
        {
            case 2:
                uiController.ChangePlayerIcom(gameObject.tag);
                break;

            case 3:
                uiController.ChangePlayerIcom(gameObject.tag);
                break;
            default:
                break;
        }

        m_jobTime = PlayerData.Instance.jobTime;
    }

    private GameObject serchTag(GameObject nowObj, string tagName)
    {
        float tmpDis = 0;           //距離用一時変数
        float nearDis = 0;          //最も近いオブジェクトの距離
        //string nearObjName = "";    //オブジェクト名称
        GameObject targetObj = null; //オブジェクト

        //タグ指定されたオブジェクトを配列で取得する
        foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
        {
            //自身と取得したオブジェクトの距離を取得
            tmpDis = Vector3.Distance(obs.transform.position, nowObj.transform.position);

            //オブジェクトの距離が近いか、距離0であればオブジェクト名を取得
            //一時変数に距離を格納
            if (nearDis == 0 || nearDis > tmpDis)
            {
                nearDis = tmpDis;
                //nearObjName = obs.name;
                targetObj = obs;
            }

        }
        //最も近かったオブジェクトを返す
        //return GameObject.Find(nearObjName);
        return targetObj;
    }

    // Update is called once per frame
    void Update () {
        //最も近かったオブジェクトを取得
        //nearObj = serchTag(gameObject, "Player");
        m_playerInput.EscapePlayerInput();
        Zombiehit();
        m_jobTime = PlayerData.Instance.jobTime;
        Debug.Log(m_jobTime);
    }

    void FixedUpdate()
    {
        Job();
        CameraMove();
    }

    void Status()
    {
        switch (m_jobNum)
        {
            case 1:
                PlayerData.Instance.playerHp = 0;
                PlayerData.Instance.playerWalkSpeed = 5f;
                PlayerData.Instance.playerDashSpeed = 7f;
                PlayerData.Instance.playerJumpPower = 1500f;
                m_Speed = PlayerData.Instance.playerWalkSpeed;
                m_flap = PlayerData.Instance.playerJumpPower;
                m_DestroyTime = PlayerData.Instance.playerDashSpeed;
                break; 
            case 2:
                PlayerData.Instance.zonbieHp = 10;
                PlayerData.Instance.zonbieAttack = 1;
                PlayerData.Instance.zonbieSpeed = 1f;
                PlayerData.Instance.jobTime = 10.0f;
                break;
            case 3:
                PlayerData.Instance.vomitHp = 10;
                PlayerData.Instance.vomitAttack = 3;
                PlayerData.Instance.vomitSpeed = 5f;
                PlayerData.Instance.jobTime = 5;
                m_Speed = PlayerData.Instance.vomitSpeed;
                break;
        }
    }

    void Job()
    {
        switch (m_jobNum)
        {
            case 1:
                if (!m_jump)
                {
                    PlayerMove();
                    PlayerButton();
                }
                break;

            case 2:
                PlayerMove();
                ZombieTime();
                PlayerData.Instance.vomitSpeed = 1f;
                PlayerData.Instance.jobTime = 5.0f;
                break;
        }
    }

    void PlayerMove()
    {
        // 右・左
        float x = Input.GetAxisRaw("Horizontal");
        float horizontal = m_playerInput.Laxis_x;
        if ((int)x != 0 || (int)horizontal != 0)
        {
            // 移動する向きを求める
            m_rigid2D.velocity = new Vector2(x * m_Speed, m_rigid2D.velocity.y);
            Vector2 temp = transform.localScale;
            temp.x = x;
            transform.localScale = temp;
        }
    }

    void ZombieAction()
    {

    }

    public void ZombieTime()
    {
        uiController.JobTime();
        if (PlayerData.Instance.jobTime <= 0)
        {
            PlayerData.Instance.m_zombieNum = 0;
        }
    }

    void Zombiehit(){

        switch (m_zombiehit)
        {
            case 1:
                //普通のゾンビと当たった場合
                m_DestroyTime -= Time.deltaTime;
                if (m_DestroyTime <= 0)
                {
                    m_jobNum = 1;
                    m_DestroyTime = 5;
                    m_zombiehit = 0;
                }

                if (m_playerInput.button_Y)
                {
                    m_jobNum = 1;
                    m_DestroyTime = 5;
                    m_zombiehit = 0;
                    PlayerData.Instance.m_zombieNum = 1;
                }
                break;

            case 2:
                //ゲロゾンビと当たった場合
                m_DestroyTime -= Time.deltaTime;
                if (m_DestroyTime <= 0)
                {
                    m_jobNum = 1;
                    m_DestroyTime = 5;
                    m_zombiehit = 0;
                }

                if (m_playerInput.button_Y)
                {
                    m_jobNum = 1;
                    m_DestroyTime = 5;
                    m_zombiehit = 0;
                    PlayerData.Instance.m_zombieNum = 2;
                }
                break;
            default:
                break;
        }
    }

    void CameraMove()
    {
        //カメラの位置を取得
        Vector3 cameraPos= m_mainCamera.transform.position;
        //プレイヤーの位置から右に4移動した位置を画面中央にする
        cameraPos.x = transform.position.x + 1;
        m_mainCamera.transform.position = cameraPos;

        //カメラ表示領域の左下をワールド座標に変換
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        //カメラ表示領域の右上をワールド座標に変換
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        //プレイヤーのポジションを取得
        Vector2 pos = transform.position;
        transform.position = pos;
    }

    void PlayerButton()
    {
        if(m_playerInput.button_A)
        {
            m_rigid2D.AddForce(Vector2.up * m_flap);
            m_jump = true;
        }

        if(m_playerInput.button_B)
        {

        }

        if (m_playerInput.button_X)
        {
            m_Speed = PlayerData.Instance.playerDashSpeed;
        }
        else
        {
            m_Speed = PlayerData.Instance.playerWalkSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D hit)
    {
        
        m_jump = false;
        if (tag == "Player")
        {
            switch (hit.gameObject.tag)
            {
                case TagName.m_zombie:
                    m_jobNum = 0;
                    m_zombiehit = 1;
                    break;

                case TagName.m_dogzombie:
                    m_jobNum = 0;
                    m_zombiehit = 2;
                    break;
            }
        }
    }
}