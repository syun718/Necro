using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public readonly static PlayerData Instance = new PlayerData();

    public int m_HP;
    public int m_zombieNum;
    public float jobTime;
    public float JobTimeDeta;
    public int jobNum;
    public int soul;

    public int playerHp;
    public float playerWalkSpeed;
    public float playerDashSpeed;
    public float playerJumpPower;

    public int zonbieHp;
    public int zonbieAttack;
    public float zonbieSpeed;

    public int vomitHp;
    public int vomitAttack;
    public float vomitSpeed;

    public int dogHp;
    public int dogAttack;
    public float dogSpeed;

    public int crowHp;
    public int crowAttack;
    public float crowSpeed;

    public int muscleHp;
    public int muscleAttack;
    public float muscleSpeed;

}
