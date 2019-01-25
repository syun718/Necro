using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalZombieMove : MonoBehaviour {

    public GameObject acidPrefab;

    // ratio = X座標にどの位置に放物線の山の頂点を持ってくるかを決める割合
    float acidSpeed, ratio;

    public float timeOut = 2.0f;

    // プレイヤー認識用
    GameObject player;

    private Rigidbody2D rb;

    // 歩くスピード
    float moveSpeed = 2.0f;

    private Vector2 pos;

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
        rb = GetComponent<Rigidbody2D>();

        // プレイヤーオブジェクトを取得
        player = GameObject.Find("Player");

        OnStart(GameObject.Find("Player").gameObject);
    }

    public void OnStart(GameObject player)
    {
        this.player = player;
    }

    void Update()
    {
        
    }

    void OnWillRenderObject() //画面内でのみ動くように(画面外で動作停止させる)
    {
        if (Camera.current.name != "SceneCamera" && Camera.current.name != "Preview Camera")
        {
            // 移動範囲の制限
            pos = transform.position;

            pos.x = Mathf.Clamp(pos.x, -4.6f, 16.0f);

            transform.position = pos;

            // 画面端で方向転換
            if (pos.x >= 16.0f || pos.x <= -4.6f)
            {
                Vector2 temp = gameObject.transform.localScale;

                temp.x *= -1;

                gameObject.transform.localScale = temp;

                //moveSpeed *= -1;
            }

            // プレイヤー発見時
            if (state == EnemyState.Chase)
            {
                StartCoroutine("ChasePlayer");
            }
            else if (state == EnemyState.Walk) // プレイヤー未発見or見失った時
            {
                rb.velocity = new Vector2(transform.localScale.x * moveSpeed, rb.velocity.y);
            }
        }
    }

    IEnumerator ChasePlayer()
    {
        // Playerの位置取得
        Vector2 targetPos = player.transform.position;

        // Playerのx座標
        float x = targetPos.x;

        // 移動はxだけ
        float y = 0;

        // 移動計算用ベクトル
        Vector2 direction = new Vector2(x - transform.position.x, y).normalized;

        yield return new WaitForSeconds(0.1f);

        // 追いかける速度の指定
        rb.velocity = direction * 0.0f;

        //StartCoroutine("AcidAttack");

        yield return new WaitForSeconds(8.0f);

        rb.velocity = direction * 2.0f;

        yield return new WaitForSeconds(2.0f);

        StopCoroutine("ChasePlayer");

    }

}
