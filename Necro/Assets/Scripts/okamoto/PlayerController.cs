using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    //最も近いオブジェクト
    public GameObject nearObj;
    public GameObject m_mainCamera;
    public GameObject m_Player;
    public GameObject m_gero;
    public GameObject m_Soul;

    SpriteRenderer MainSpriteRenderer;

    public Sprite[] icon_Sprite = new Sprite[0];

    public Transform muzzle;

    Camera cam;
    Rigidbody2D m_rigid2D;
    Vector2 m_vector;
    PlayerInput m_playerInput;
   
    ChangePlayer m_changePlayer;
    PlayerAnimations m_playerAnimations;
    public Animator animator;

    public float m_Speed;
    public float firstSpeed;
    public float m_flap;
    public float m_jobTime;
    public float m_DestroyTime;

    int playerStock;

    bool m_jump = false;
    bool m_Move = true;
    bool m_Zombiehit = false;

    private UiController uiController;

    void Start () {
        Status();
        playerStock = PlayerData.Instance.playerStock;
        m_vector = Vector2.zero;
        PlayerData.Instance.SetJobTime = PlayerData.Instance.jobTime;
        m_playerInput = GetComponent<PlayerInput>();
        cam = m_mainCamera.GetComponent<Camera>();
        m_rigid2D = GetComponent<Rigidbody2D>();
        m_playerAnimations = GetComponent<PlayerAnimations>();
        animator = GetComponent<Animator>();
        MainSpriteRenderer = GetComponent<SpriteRenderer>();
        uiController = GameObject.Find("UiController").GetComponent<UiController>();
        m_changePlayer = GameObject.Find("PlayerManager").GetComponent<ChangePlayer>();
        PlayerData.Instance.SetJobTime = PlayerData.Instance.jobTime;

        switch (gameObject.tag)
        {
            case TagName.zombie:
                uiController.ChangePlayerIcom(gameObject.tag);
                break;

            case TagName.gelozombie:
                uiController.ChangePlayerIcom(gameObject.tag);
                break;

            default:
                break;
        }

        m_jobTime = PlayerData.Instance.jobTime;
    }

    private GameObject serchTag(GameObject nowObj, string tagName)
    {
<<<<<<< HEAD
        float tmpDis = 0;           //距離用一時変数
        float nearDis = 2;          //最も近いオブジェクトの距離
        GameObject targetObj = null; //オブジェクト

   
        //タグ指定されたオブジェクトを配列で取得する
=======
        //距離用一時変数
        float tmpDis = 0;
        //最も近いオブジェクトの距離
        float nearDis = 10;
        //オブジェクト
        GameObject targetObj = null; 

        //タグ指定されたオブジェクトを配列で取得する
>>>>>>> okamoto
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
        return targetObj;
    }

    void Update () {
        m_playerInput.EscapePlayerInput();
        m_jobTime = PlayerData.Instance.jobTime;
<<<<<<< HEAD
        //Debug.Log(m_jobTime);
=======
        Debug.Log(playerStock);
>>>>>>> okamoto

        if(m_Zombiehit)
        {
            ZombiehitSet();
        }
    }

    void FixedUpdate()
    {
        Job();
        CameraMove();
    }

    void Status()
    {
        switch (LayerMask.LayerToName(gameObject.layer))
        {
            case LayerName.player:
                PlayerData.Instance.playerStock = 5;
                firstSpeed = PlayerData.Instance.playerWalkSpeed;
                m_Speed = firstSpeed;
                m_flap = PlayerData.Instance.playerJumpPower;
                m_DestroyTime = PlayerData.Instance.playerDashSpeed;
                break;

            case LayerName.gelozombie:
                PlayerData.Instance.jobTime = 5f;
                firstSpeed = PlayerData.Instance.vomitSpeed;
                m_Speed = firstSpeed;
                m_flap = PlayerData.Instance.playerJumpPower;
                break;

            case LayerName.powerzombie:
                PlayerData.Instance.jobTime = 15f;
                firstSpeed = PlayerData.Instance.muscleSpeed;
                m_flap = PlayerData.Instance.playerJumpPower;
                m_Speed = firstSpeed;
                break;

            case LayerName.dogzombie:
                PlayerData.Instance.jobTime = 10f;
                firstSpeed = PlayerData.Instance.dogSpeed;
                m_flap = PlayerData.Instance.playerJumpPower;
                m_Speed = firstSpeed;
                break;

            case LayerName.birdzombie:
                PlayerData.Instance.jobTime = 5f;
                firstSpeed = PlayerData.Instance.crowSpeed;
                m_Speed = firstSpeed;
                break;

            default:
                break;
        }
    }

    void Job()
    {
        switch (gameObject.tag)
        {
<<<<<<< HEAD
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
=======
            case TagName.player:
                PlayerButton();
                PlayerMove();
                //最も近かったオブジェクトを取得
                nearObj = serchTag(gameObject, TagName.zombie);
                m_changePlayer.charaLists[1] = nearObj;
>>>>>>> okamoto
                break;

            case TagName.zombie:
                PlayerMove();
                ZombieButton();
                ZombieTime();
                break;
<<<<<<< HEAD

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
=======

            default:
>>>>>>> okamoto
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
            temp.x = (float)x / 10f;
            transform.localScale = temp;
        }
        m_playerAnimations.MoveAnimation(horizontal);
    }

    public void ZombieTime()
    {
        uiController.JobTime();
        if (PlayerData.Instance.jobTime <= 0)
        {
            if (PlayerData.Instance.playerStock != 0)
            {
                PlayerData.Instance.jobTime = PlayerData.Instance.SetJobTime;
                PlayerData.Instance.m_zombieNum = 0;
                m_changePlayer.ChangeCharacter(m_changePlayer.nowChara);
                PlayerPosition();
                m_Player.SetActive(true);
                gameObject.SetActive(false);
            } else {
                gameObject.SetActive(false);
                Debug.Log("END");
            }
        }
    }

    void PlayerPosition()
    {
        var playerPos = transform.position;
        playerPos.x = cam.transform.position.x;
        m_Player.transform.position = playerPos;
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
        if(m_playerInput.button_A && !m_jump)
        {
            m_rigid2D.AddForce(Vector2.up * m_flap);
            m_playerAnimations.JumpAnimation();
            m_jump = true;
        }

        if(m_playerInput.button_B)
        {

        }

        if(m_playerInput.button_X)
        {
            m_Speed = PlayerData.Instance.playerDashSpeed;
        }
        else
        {
            m_Speed = firstSpeed;
        }

        if (m_Zombiehit)
        {
            ZombiehitSet();
        }
    }

    void ZombiehitSet()
    {

        //普通のゾンビと当たった場合
        m_DestroyTime -= Time.deltaTime;
        if (m_DestroyTime <= 0)
        {
            m_DestroyTime = 5;
            MainSpriteRenderer.enabled = true;
            m_Soul.SetActive(false);
            PlayerData.Instance.playerStock -= 1;
            m_Zombiehit = false;
        }

        if (m_playerInput.button_Y)
        {
            m_DestroyTime = 5;
            MainSpriteRenderer.enabled = true;
            PlayerData.Instance.m_zombieNum = 1;
            m_changePlayer.ChangeCharacter(m_changePlayer.nowChara);
            PlayerData.Instance.playerStock -= 1;
            m_Player.SetActive(false);
            m_Soul.SetActive(false);
            m_Zombiehit = false;
        }
    }

    void ZombieButton()
    {
        switch(LayerMask.LayerToName(gameObject.layer))
        {
            case LayerName.gelozombie:
                if(m_playerInput.button_A && !m_jump)
                {
                    m_rigid2D.AddForce(Vector2.up * m_flap);
                    m_playerAnimations.JumpAnimation();
                    m_jump = true;
                }

                if (m_playerInput.button_B)
                {
                    // ゲロの複製
                    GameObject bullets = Instantiate(m_gero) as GameObject;

                    // ゲロの位置を調整
                    bullets.transform.position = muzzle.position;
                    m_playerAnimations.GeroAnimation();
                }
                break;

            case LayerName.powerzombie:
                if (m_playerInput.button_A && !m_jump)
                {
                    m_rigid2D.AddForce(Vector2.up * m_flap);
                    m_playerAnimations.JumpAnimation();
                    m_jump = true;
                }
                if (m_playerInput.button_B)
                {
                    
                }
                break;

            case LayerName.dogzombie:
                if (m_playerInput.button_A && !m_jump)
                {
                    m_rigid2D.AddForce(Vector2.up * m_flap);
                    m_playerAnimations.JumpAnimation();
                    m_jump = true;
                }
                if (m_playerInput.button_B)
                {

                }
                break;

            case LayerName.birdzombie:
                if (m_playerInput.button_A && !m_jump)
                {
                    m_rigid2D.AddForce(Vector2.up * m_flap);
                    m_playerAnimations.JumpAnimation();
                    m_jump = true;
                }
                if (m_playerInput.button_B)
                {

                }
                break;

            default:
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D hit)
    {
        
        m_jump = false;
        if (gameObject.tag == TagName.player)
        {
            Zombiehit(hit);
        }
    }

    void Zombiehit(Collision2D hit)
    {
        switch (hit.gameObject.tag)
        {
            case TagName.zombie:
                m_Move = false;
                m_Soul.SetActive(true);
                MainSpriteRenderer.enabled = false;
                m_Zombiehit = true;
                break;

            default:
                break;
        }
    }
}