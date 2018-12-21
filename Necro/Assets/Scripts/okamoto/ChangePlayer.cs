using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayer : MonoBehaviour {

    PlayerController m_playerController;
    //操作可能なゲームキャラクター
    public List<GameObject> charaLists;
    //現在どのキャラクターを操作しているか
    public int nowChara;

    void Start()
    {
        //最初の操作キャラクターを0番目のキャラクターにする為、キャラクターの総数をnowCharaに設定し最初のキャラクターが設定されるようにする
        nowChara = charaLists.Count;
    }

    //　操作キャラクター変更メソッド
    public void ChangeCharacter(int tempNowChara)
    {

        bool flag;  //オン・オフのフラグ
        //次の操作キャラクターを指定
        //次の操作キャラクターがいない時は最初のキャラクターに設定
        if (PlayerData.Instance.m_zombieNum >= charaLists.Count)
        {
            PlayerData.Instance.m_zombieNum = 0;
        }
        //次の操作キャラクターだったら動く機能を有効にし、それ以外は無効にする
        for (var i = 0; i < charaLists.Count; i++)
        {
            if (i == PlayerData.Instance.m_zombieNum)
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
            //操作するキャラクターと操作しないキャラクターで機能のオン・オフをする
            charaLists[i].GetComponent<PlayerController>().enabled = flag;
        }
        //次の操作キャラクターを現在操作しているキャラクターに設定して終了
        nowChara = PlayerData.Instance.m_zombieNum;
    }


}