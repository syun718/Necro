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

    public enum PlayerJob
    {
        Player,
        GeroZomie,
        Dogzombie,
        PowerZombie,
        BirdZombie
    }

    public PlayerJob m_job;

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
        string JobName = m_job.ToString();

        switch (m_job)
        {
            case PlayerJob.GeroZomie:
                uiController.ChangePlayerIcom(JobName);
                break;

            //case TagName.gelozombie:
                //uiController.ChangePlayerIcom(gameObject.tag);
                //break;

            default:
                break;
        }

        m_jobTime = PlayerData.Instance.jobTime;
    }

    private GameObject serchTag(GameObject nowObj, string tagName)
    {
        //距離用一時変数
        float tmpDis = 0;
        //最も近いオブジェクトの距離
        float nearDis = 10;
        //オブジェクト
        GameObject targetObj = null; 

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
        return targetObj;
    }

    void Update () {
        m_playerInput.EscapePlayerInput();
        m_jobTime = PlayerData.Instance.jobTime;
        Debug.Log(playerStock);

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
        switch (m_job)
        {
            case PlayerJob.Player:
                PlayerData.Instance.playerStock = 5;
                firstSpeed = PlayerData.Instance.playerWalkSpeed;
                m_Speed = firstSpeed;
                m_flap = PlayerData.Instance.playerJumpPower;
                m_DestroyTime = PlayerData.Instance.playerDashSpeed;
                break;

            case PlayerJob.GeroZomie:
                PlayerData.Instance.jobTime = 5f;
                firstSpeed = PlayerData.Instance.vomitSpeed;
                m_Speed = firstSpeed;
                m_flap = PlayerData.Instance.playerJumpPower;
                tag = TagName.player;
                break;

            case PlayerJob.PowerZombie:
                PlayerData.Instance.jobTime = 15f;
                firstSpeed = PlayerData.Instance.muscleSpeed;
                m_flap = PlayerData.Instance.playerJumpPower;
                m_Speed = firstSpeed;
                tag = TagName.player;
                break;

            case PlayerJob.Dogzombie:
                PlayerData.Instance.jobTime = 15f;
                firstSpeed = PlayerData.Instance.dogSpeed;
                m_flap = PlayerData.Instance.playerJumpPower;
                m_Speed = firstSpeed;
                tag = TagName.player;
                break;

            case PlayerJob.BirdZombie:
                PlayerData.Instance.jobTime = 5f;
                firstSpeed = PlayerData.Instance.crowSpeed;
                m_Speed = firstSpeed;
                tag = TagName.player;
                break;

            default:
                break;
        }
    }

    void Job()
    {
        switch (m_job)
        {
            case PlayerJob.Player:
                PlayerButton();
                PlayerMove();
                //最も近かったオブジェクトを取得
                nearObj = serchTag(gameObject, TagName.zombie);
                m_changePlayer.charaLists[1] = nearObj;
                break;

            case PlayerJob.GeroZomie:
                PlayerMove();
                ZombieButton();
                ZombieTime();
                break;

            case PlayerJob.PowerZombie:
                PlayerMove();
                ZombieButton();
                ZombieTime();
                break;

            case PlayerJob.Dogzombie:
                PlayerMove();
                ZombieButton();
                ZombieTime();
                break;

            case PlayerJob.BirdZombie:
                PlayerMove();
                ZombieButton();
                ZombieTime();
                break;

            default:
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
            var direction = new Vector3(0, 0, x);
            transform.localRotation = Quaternion.LookRotation(direction);
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
        cameraPos.y = transform.position.y + 1;
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
            m_DestroyTime = 10;
            PlayerData.Instance.playerStock -= 1;
            m_Zombiehit = false;
        }

        if (m_playerInput.button_Y)
        {
            m_DestroyTime = 10;
            PlayerData.Instance.m_zombieNum = 1;
            m_changePlayer.ChangeCharacter(m_changePlayer.nowChara);
            PlayerData.Instance.playerStock -= 1;
            m_Player.SetActive(false);
            m_Zombiehit = false;
        }
    }

    void ZombieButton()
    {
        switch(m_job)
        {
            case PlayerJob.GeroZomie:
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

                if(m_playerInput.button_X)
                {

                }

                if (m_playerInput.button_Y)
                {

                }
                break;

            case PlayerJob.PowerZombie:
                if (m_playerInput.button_A && !m_jump)
                {
                    m_rigid2D.AddForce(Vector2.up * m_flap);
                    m_playerAnimations.JumpAnimation();
                    m_jump = true;
                }
                if (m_playerInput.button_B)
                {
                    
                }

                if (m_playerInput.button_X)
                {

                }

                if (m_playerInput.button_Y)
                {

                }
                break;

            case PlayerJob.Dogzombie:
                if (m_playerInput.button_A && !m_jump)
                {
                    m_rigid2D.AddForce(Vector2.up * m_flap);
                    m_playerAnimations.JumpAnimation();
                    m_jump = true;
                }
                if (m_playerInput.button_B)
                {

                }

                if (m_playerInput.button_X)
                {

                }

                if (m_playerInput.button_Y)
                {

                }
                break;

            case PlayerJob.BirdZombie:
                if (m_playerInput.button_A && !m_jump)
                {
                    m_rigid2D.AddForce(Vector2.up * m_flap);
                    m_playerAnimations.JumpAnimation();
                    m_jump = true;
                }
                if (m_playerInput.button_B)
                {

                }

                if (m_playerInput.button_X)
                {

                }

                if (m_playerInput.button_Y)
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
        if (m_job == PlayerJob.Player)
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