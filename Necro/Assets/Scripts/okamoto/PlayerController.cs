using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    //最も近いオブジェクト
    public GameObject[] nearObj;
    public GameObject m_mainCamera;

    Rigidbody2D m_rigid2D;
    PlayerInput m_playerInput;
    ChangePlayer m_changePlayer;
    PlayerAnimations m_playerAnimations;
    public Animator animator;

    public float m_Speed;
    public float firstSpeed;
    public float m_flap;
    public float m_jobTime;
    public float m_DestroyTime;

    public int m_zombiehit;

    bool m_jump = false;
    bool m_MoveFlag = true;

    private UiController uiController;

    // Use this for initialization
    void Start () {
        PlayerData.Instance.playerHp = 0;
        Status();
        PlayerData.Instance.SetJobTime = PlayerData.Instance.jobTime;
        m_playerInput = GetComponent<PlayerInput>();
        m_rigid2D = GetComponent<Rigidbody2D>();
        m_playerAnimations = GetComponent<PlayerAnimations>();
        animator = GetComponent<Animator>();
        uiController = GameObject.Find("UiController").GetComponent<UiController>();
        m_changePlayer = GameObject.Find("PlayerManager").GetComponent<ChangePlayer>();
        Status();
        PlayerData.Instance.SetJobTime = PlayerData.Instance.jobTime;

        switch (gameObject.tag)
        {
            case TagName.m_zombie:
                uiController.ChangePlayerIcom(gameObject.tag);
                break;

            case TagName.m_gelozombie:
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
        float nearDis = 2;          //最も近いオブジェクトの距離
        GameObject targetObj = null; //オブジェクト

   
        //タグ指定されたオブジェクトを配列で取得する
        foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
        {
        
                //自身と取得したオブジェクトの距離を取得
                tmpDis = Vector3.Distance(obs.transform.position, nowObj.transform.position);

                //オブジェクトの距離が近いか、距離0であればオブジェクト名を取得
                //一時変数に距離を格納
                if (nearDis > tmpDis)
                {
                    nearDis = tmpDis;
                    //nearObjName = obs.name;
                    targetObj = obs;
                }

        }
        Debug.Log("targetObj: " + targetObj);
        return targetObj;
    }

    // Update is called once per frame
    void Update () {
        m_playerInput.EscapePlayerInput();
        Zombiehit();
        m_jobTime = PlayerData.Instance.jobTime;
        //Debug.Log(m_jobTime);

    }

    void FixedUpdate()
    {
        Job();
        CameraMove();
    }

    void Status()
    {
        switch (gameObject.tag)
        {
            case TagName.m_player:
                PlayerData.Instance.playerHp = 0;
                PlayerData.Instance.playerWalkSpeed = 5f;
                PlayerData.Instance.playerDashSpeed = 7f;
                PlayerData.Instance.playerJumpPower = 1500f;
                firstSpeed = PlayerData.Instance.playerWalkSpeed;
                m_Speed = firstSpeed;
                m_flap = PlayerData.Instance.playerJumpPower;
                m_DestroyTime = PlayerData.Instance.playerDashSpeed;
                break;

            case TagName.m_zombie:
                PlayerData.Instance.zonbieHp = 10;
                PlayerData.Instance.zonbieAttack = 1;
                PlayerData.Instance.zonbieSpeed = 3f;
                PlayerData.Instance.jobTime = 10.0f;
                firstSpeed = PlayerData.Instance.zonbieSpeed;
                break;

            case TagName.m_gelozombie:
                PlayerData.Instance.vomitHp = 10;
                PlayerData.Instance.vomitAttack = 3;
                PlayerData.Instance.vomitSpeed = 4f;
                PlayerData.Instance.jobTime = 5f;
                firstSpeed = PlayerData.Instance.vomitSpeed;
                m_Speed = firstSpeed;
                m_flap = PlayerData.Instance.playerJumpPower;
                break;

            case TagName.m_powerzombie:
                PlayerData.Instance.muscleHp = 10;
                PlayerData.Instance.muscleAttack = 5;
                PlayerData.Instance.muscleSpeed = 5f;
                firstSpeed = PlayerData.Instance.muscleSpeed;
                m_Speed = firstSpeed;
                break;

            case TagName.m_dogzombie:
                PlayerData.Instance.dogHp = 5;
                PlayerData.Instance.dogAttack = 5;
                PlayerData.Instance.dogSpeed = 10f;
                firstSpeed = PlayerData.Instance.dogSpeed;
                m_Speed = firstSpeed;
                break;

            case TagName.m_birdzombie:
                PlayerData.Instance.crowHp = 5;
                PlayerData.Instance.crowAttack = 5;
                PlayerData.Instance.crowSpeed = 5f;
                firstSpeed = PlayerData.Instance.crowSpeed;
                m_Speed = firstSpeed;
                break;
        }
    }

    void Job()
    {
        switch (gameObject.tag)
        {
            case TagName.m_player:
                //if (!m_jump)
                //{
                    if (m_MoveFlag)
                    {
                        PlayerMove();
                    }
                    PlayerButton();
                //}

                //最も近かったオブジェクトを取得
                nearObj[0] = serchTag(gameObject, TagName.m_zombie);
                //最も近かったオブジェクトを取得
                nearObj[1] = serchTag(gameObject, TagName.m_gelozombie);
                //最も近かったオブジェクトを取得
                nearObj[2] = serchTag(gameObject, TagName.m_powerzombie);
                //最も近かったオブジェクトを取得
                nearObj[3] = serchTag(gameObject, TagName.m_dogzombie);
                //最も近かったオブジェクトを取得
                nearObj[4] = serchTag(gameObject, TagName.m_birdzombie);
                break;

            case TagName.m_zombie:
                PlayerMove();
                ZombieTime();
                break;

            case TagName.m_gelozombie:
                if (!m_jump)
                {
                    PlayerMove();
                    ZombieButton();
                }
                ZombieTime();
                break;

            case TagName.m_powerzombie:
                PlayerMove();
                ZombieButton();
                ZombieTime();
                break;

            case TagName.m_dogzombie:
                if (!m_jump)
                {
                    PlayerMove();
                    ZombieButton();
                }
                ZombieTime();
                break;

            case TagName.m_birdzombie:
                if (!m_jump)
                {
                    PlayerMove();
                    ZombieButton();
                }
                ZombieTime();
                break;
        }
    }

    void PlayerMove()
    {
        // 右・左
        //float x = Input.GetAxisRaw("Horizontal");
        float horizontal = m_playerInput.Laxis_x;
        if ((int)horizontal != 0)
        {
            // 移動する向きを求める
            m_rigid2D.velocity = new Vector2(horizontal * m_Speed, m_rigid2D.velocity.y);
            Vector2 temp = transform.localScale;
            temp.x = (float)horizontal / 10f;
            transform.localScale = temp;
            m_playerAnimations.MoveAnimation(horizontal);
        }
    }

    public void ZombieTime()
    {
        uiController.JobTime();
        if (PlayerData.Instance.jobTime <= 0)
        {
            PlayerData.Instance.jobTime = PlayerData.Instance.SetJobTime;
            PlayerData.Instance.m_zombieNum = 0;
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// m_zombiehit = 1が普通のゾンビ、2がゲロゾンビ、3がパワーゾンビ、４が犬ゾンビ、5が鳥ゾンビ
    /// </summary>
    void Zombiehit(){

        switch (m_zombiehit)
        {
            case 1:
                //普通のゾンビと当たった場合
                m_DestroyTime -= Time.deltaTime;
                if (m_DestroyTime <= 0)
                {
                    m_DestroyTime = 5;
                    m_zombiehit = 0;
                }

                if (m_playerInput.button_Y)
                {
                    m_DestroyTime = 5;
                    m_zombiehit = 0;
                    PlayerData.Instance.m_zombieNum = 1;
                    m_changePlayer.ChangeCharacter(m_changePlayer.nowChara);
                    m_MoveFlag = true;
                }
                break;

            case 2:
                //ゲロゾンビと当たった場合
                m_DestroyTime -= Time.deltaTime;
                if (m_DestroyTime <= 0)
                {
                    m_DestroyTime = 5;
                    m_zombiehit = 0;
                }

                if (m_playerInput.button_Y)
                {
                    m_DestroyTime = 5;
                    m_zombiehit = 0;
                    PlayerData.Instance.m_zombieNum = 1;
                    m_MoveFlag = true;
                }
                break;

            case 3:
                //パワーゾンビ
                m_DestroyTime -= Time.deltaTime;
                if (m_DestroyTime <= 0)
                {
                    m_DestroyTime = 5;
                    m_zombiehit = 0;
                }

                if (m_playerInput.button_Y)
                {
                    m_DestroyTime = 5;
                    m_zombiehit = 0;
                    PlayerData.Instance.m_zombieNum = 1;
                    m_MoveFlag = true;
                }
                break;

            case 4:
                //犬ゾンビ
                m_DestroyTime -= Time.deltaTime;
                if (m_DestroyTime <= 0)
                {
                    m_DestroyTime = 5;
                    m_zombiehit = 0;
                }

                if (m_playerInput.button_Y)
                {
                    m_DestroyTime = 5;
                    m_zombiehit = 0;
                    PlayerData.Instance.m_zombieNum = 1;
                    m_MoveFlag = true;
                }
                break;

            case 5:
                //鳥ゾンビ
                m_DestroyTime -= Time.deltaTime;
                if (m_DestroyTime <= 0)
                {
                    m_DestroyTime = 5;
                    m_zombiehit = 0;
                }

                if (m_playerInput.button_Y)
                {
                    m_DestroyTime = 5;
                    m_zombiehit = 0;
                    PlayerData.Instance.m_zombieNum = 1;
                    m_MoveFlag = true;
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
        cameraPos.y = transform.position.y + 2;
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
            m_playerAnimations.JumpAnimation();
            animator.SetTrigger("Jump");
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
            m_Speed = firstSpeed;
        }
    }

    void ZombieButton()
    {
        switch(gameObject.tag)
        {
            case TagName.m_gelozombie:
                if(m_playerInput.button_A)
                {
                    m_rigid2D.AddForce(Vector2.up * m_flap);
                    m_jump = true;
                }
                break;

            case TagName.m_powerzombie:
                if(m_playerInput.button_B)
                {
                    
                }
                break;

            case TagName.m_dogzombie:

                break;

            case TagName.m_birdzombie:

                break;
        }
    }

    void OnCollisionEnter2D(Collision2D hit)
    {
        
        m_jump = false;
        if (tag == TagName.m_player)
        {
            switch (hit.gameObject.tag)
            {
                case TagName.m_zombie:
                    m_changePlayer.charaLists[1] = nearObj[0];
                    m_MoveFlag = false;
                    m_zombiehit = 1;
                    break;

                case TagName.m_gelozombie:
                    m_changePlayer.charaLists[1] = nearObj[1];
                    m_MoveFlag = false;
                    m_zombiehit = 2;
                    break;

                case TagName.m_powerzombie:
                    m_changePlayer.charaLists[1] = nearObj[2];
                    m_MoveFlag = false;
                    m_zombiehit = 3;
                    break;

                case TagName.m_dogzombie:
                    m_changePlayer.charaLists[1] = nearObj[3];
                    m_MoveFlag = false;
                    m_zombiehit = 4;
                    break;

                case TagName.m_birdzombie:
                    m_changePlayer.charaLists[1] = nearObj[4];
                    m_MoveFlag = false;
                    m_zombiehit = 5;
                    break;
            }
        }
    }
}