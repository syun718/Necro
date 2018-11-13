using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //最も近いオブジェクト
    public GameObject nearObj;
    public GameObject m_mainCamera;

    Rigidbody2D m_rigid2D;
    PlayerInput m_playerInput;

    public float m_Speed;
    public float m_flap;
    public float m_jobTime;

    public int m_jobNum;

    bool m_jump = false;

    // Use this for initialization
    void Start () {
        PlayerData.Instance.m_HP = 0;
        m_Speed = 5f;
        m_flap = 1500f;
        m_jobTime = 3;

        //最も近かったオブジェクトを取得
        nearObj = serchTag(gameObject, "Player");
        m_playerInput = GetComponent<PlayerInput>();
        m_rigid2D = GetComponent<Rigidbody2D>();

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
        nearObj = serchTag(gameObject, "Player");
        m_playerInput.EscapePlayerInput();
    }

    void FixedUpdate()
    {
        switch (m_jobNum)
        {
            case 0:
                if (!m_jump)
                {
                    PlayerMove();
                    PlayerButton();
                }
                break;

            case 1:
                PlayerMove();
                ZombieTime();
                break;
        }

        CameraMove();
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
            temp.x = (int)horizontal;
            transform.localScale = temp;
        }
    }

    void ZombieAction()
    {

    }

    void ZombieTime()
    {
        m_jobTime -= Time.deltaTime;
        if(m_jobTime <= 0)
        {
            PlayerData.Instance.m_zombieNum = 0;
            m_jobTime = 3;
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
            m_Speed = 7f;
        }
        else
        {
            m_Speed = 5f;
        }

        if(PlayerData.Instance.m_HP == 0)
        {
            if (m_playerInput.button_Y)
            {

            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        m_jump = false;

        if (PlayerData.Instance.m_HP == 0)
        {
            switch (other.gameObject.tag)
            {
                case TagName.m_zombie:
                    PlayerData.Instance.m_zombieNum = 1;
                    break;

                case TagName.m_dogzombie:
                    PlayerData.Instance.m_zombieNum = 2;
                    break;
            }
        }
    }
}
