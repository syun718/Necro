using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SoulShot : MonoBehaviour {

    //public GameObject shotSoul;
    //最も近いオブジェクト
    public GameObject nearObj;

    bool shotFlag = true;

    public float soulSpeed = 5f;

    ChangePlayer m_changePlayer;
    private Rigidbody2D rb2d;
    private Renderer targetRenderer;
    private PlayerController zombie;
    private PlayerController player;
    private UiController uiController;


    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        targetRenderer = GetComponent<Renderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        uiController = GameObject.Find("UiController").GetComponent<UiController>();
        m_changePlayer = GameObject.Find("PlayerManager").GetComponent<ChangePlayer>();
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

    // Update is called once per frame
    void Update () {
        Shot();
        //最も近かったオブジェクトを取得
        nearObj = serchTag(gameObject, TagName.zombie);
        m_changePlayer.charaLists[1] = nearObj;
    }


    public void Shot()
    {
        Vector2 bulletMovement = transform.right;
        rb2d.velocity = bulletMovement * soulSpeed;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
        if(player.m_ShotSoul == false)
        {
            player.m_ShotSoul = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
        else if(col.gameObject.tag == TagName.zombie)
        {
            col.gameObject.GetComponent<ZombieController>().Hijack_Frag = true;
            PlayerData.Instance.m_zombieNum = 1;
            uiController.destroyCount = 0;
            if (uiController.destroyCount == 0)
            {
                uiController.life[0].SetActive(true);
                uiController.life[1].SetActive(true);
                uiController.life[2].SetActive(true);
            }
            m_changePlayer.ChangeCharacter(m_changePlayer.nowChara);
            player.gameObject.SetActive(false);
            Destroy(gameObject);
        }
        
    }


}
