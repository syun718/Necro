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

        if (m_PlayerInput.Laxis_x >= 0.1f || m_PlayerInput.Laxis_x <= -0.1f)
        {
            Debug.Log("あ");
            if (m_PlayerController.m_Speed <= m_PlayerController.firstSpeed)
            {
                m_animatorManeger.MoveSpeed = 0.5f;
                animator.SetFloat("Move", 0.5f);
            }
            else if (m_PlayerController.m_Speed >= m_PlayerController.firstSpeed)
            {
                m_animatorManeger.MoveSpeed = 1f;
                animator.SetFloat("Move", 1f);
            }
        }
        else
        {
            m_animatorManeger.MoveSpeed = 0f;
            animator.SetFloat("Move", 0f);
        }

        if (m_action)
        {
            m_PlayerController.m_Speed = 0;
            animator.SetFloat("Move", 0f);
        }
    }

    //突き飛ばされた時のアニメーション
    public void JumpAnimation()
    {
        m_action = true;
        //アニメーション再生後の処理
        StartCoroutine(ActionAnimation(() =>
        {
            m_action = false;
        }));
    }

    IEnumerator ActionAnimation(System.Action callback)
    {
        yield return null;
        yield return new WaitForSeconds(1f);
        callback();
    }
}
