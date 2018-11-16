using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalZombieMove : MonoBehaviour {

    // プレイヤー認識用
    GameObject player;

    private Rigidbody rb;

    // 歩くスピード
    float moveSpeed = 3.0f;

    private Vector3 pos;

    // NormalZombieの状態
    private EnemyState state;

    public enum EnemyState
    {
        Walk, // プレイヤー未発見or見失い時
        Chase // プレイヤー発見&追跡時
    };

    // NormalZombieの状態変更メソッド
    public void SetState(string mode, Transform obj = null)
    {
        if(mode == "walk")
        {
            state = EnemyState.Walk;
        } else if(mode == "chase")
        {
            state = EnemyState.Chase;
        }
    }

    public EnemyState GetState()
    {
        return state;
    }

    void Start ()
    {
        rb = GetComponent<Rigidbody>();

        // プレイヤーオブジェクトを取得
        player = GameObject.Find("Player(Temp)");
	}

    //void Update ()

    void OnWillRenderObject()
    {
        if (Camera.current.name != "SceneCamera" && Camera.current.name != "Preview Camera")
        {
            // 移動範囲の制限
            pos = transform.position;

            pos.x = Mathf.Clamp(pos.x, -8.4f, 8.4f);

            transform.position = pos;

            // 画面端で方向転換
            if (pos.x >= 8.4f || pos.x <= -8.4f)
            {
                Vector3 temp = gameObject.transform.localScale;

                moveSpeed *= -1;
            }

            // プレイヤー発見時
            if (state == EnemyState.Chase)
            {
                // Playerの位置取得
                Vector3 targetPos = player.transform.position;

                // Playerのx座標
                float x = targetPos.x;

                // 移動はxだけ
                float y = 0;
                float z = 0;

                // 移動計算用ベクトル
                Vector3 direction = new Vector3(x - transform.position.x, y, z).normalized;

                // 追いかける速度の指定
                rb.velocity = direction * 1.5f;
            }
            else if (state == EnemyState.Walk) // プレイヤー未発見or見失った時
            {
                rb.velocity = new Vector3(transform.localScale.x * moveSpeed, rb.velocity.y, rb.velocity.z);
            }
        }
    }

}
