using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Animator管理クラス
/// </summary>
[System.Serializable]
public class AnimatorManager
{
    public enum ParamName
    {
        PlayerMove,
        Jump,
        Gero,
        Attack,

    }

    static readonly Dictionary<ParamName, int> m_paramHashDete = new Dictionary<ParamName, int>()
    {
        { ParamName.PlayerMove,  Animator.StringToHash("PlayerMove") },
        { ParamName.Jump,        Animator.StringToHash("Jump")},
        { ParamName.Gero,      Animator.StringToHash("Gero")},
        { ParamName.Attack,      Animator.StringToHash("Attack")},

    };

    public enum StateName
    {
        Idle,
        Move,
        Jump,
        gero_gero,
        Attack,
    }

    static readonly Dictionary<StateName, int> m_stateHashDate = new Dictionary<StateName, int>()
    {
        { StateName.Idle,       Animator.StringToHash("Base Layer.Idle") },
        { StateName.Move,       Animator.StringToHash("Base Layer.Move") },
        { StateName.Jump,       Animator.StringToHash("Bass Leyer.Jump")},
        { StateName.gero_gero,     Animator.StringToHash("Bass Leyer.gero_gero")},

    };

    List<Animator> m_animatorList;

    public AnimatorManager(Animator[] m_animator)
    {
        m_animatorList = new List<Animator>(m_animator);
    }

    public Animator ActiveComponent
    {
        get
        {
            var ret = m_animatorList.Find(a => a.gameObject.activeSelf);
            return ret ?? m_animatorList[0];
        }
    }

    public bool IsActive
    {
        get
        {
            return ActiveComponent.gameObject.activeSelf;
        }
    }

    public int GetStateHash(StateName m_stateName)
    {
        return m_stateHashDate[m_stateName];
    }

    public int CurrentStateHash
    {
        get
        {
            return ActiveComponent.GetCurrentAnimatorStateInfo(0).fullPathHash;
        }
    }

    public float MoveSpeed
    {
        get
        {
            return ActiveComponent.GetFloat(m_paramHashDete[ParamName.PlayerMove]);
        }
        set
        {
            if (IsActive)
            {
                ActiveComponent.SetFloat(m_paramHashDete[ParamName.PlayerMove], value);
            }
        }
    }

    public void SetJump()
    {
        if (IsActive)
        {
            ActiveComponent.SetTrigger(m_paramHashDete[ParamName.Jump]);
        }
    }

    public void SetGero_Gero()
    {
        if (IsActive)
        {
            ActiveComponent.SetTrigger(m_paramHashDete[ParamName.Gero]);
        }
    }

    public void SetAtteck()
    {
        if (IsActive)
        {
            ActiveComponent.SetTrigger(m_paramHashDete[ParamName.Attack]);
        }
    }


}
