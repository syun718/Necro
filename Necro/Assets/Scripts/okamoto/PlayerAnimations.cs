using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour {

    PlayerInput m_PlayerInput;
    PlayerController m_PlayerController;
    AnimatorManager m_animatorManeger;
    public Animator animator;


    public bool m_action;

    // Use this for initialization
    void Start () {
        m_PlayerInput = GetComponent<PlayerInput>();
        m_PlayerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        m_animatorManeger = new AnimatorManager(GetComponentsInChildren<Animator>(true));
    }

    //走っている時のアニメーション
    public void MoveAnimation(float horizontal)
    {
        float x = Input.GetAxisRaw("Horizontal");
        if (m_PlayerInput.Laxis_x != 0 || x != 0)
        {
            if (m_PlayerController.m_Speed <= m_PlayerController.firstSpeed)
            {
                m_animatorManeger.MoveSpeed = 0.5f;
            }
            else if (m_PlayerController.m_Speed >= m_PlayerController.firstSpeed)
            {
                m_animatorManeger.MoveSpeed = 1f;
            }
        }
        else
        {
            m_animatorManeger.MoveSpeed = 0f;
        }
    }

    //ジャンプアニメーション
    public void JumpAnimation()
    {
        m_animatorManeger.SetJump();
    }

    //ゲロアニメーション
    public void GeroAnimation()
    {
        m_action = true;
        m_animatorManeger.SetGero_Gero();
        //アニメーション再生後の処理
        StartCoroutine(ActionAnimation(() =>
        {

        }));
    }

    IEnumerator ActionAnimation(System.Action callback)
    {
        yield return null;
        yield return new WaitForSeconds(1f);
        callback();
    }
}
