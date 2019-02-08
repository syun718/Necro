using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public readonly static PlayerData Instance = new PlayerData();

    public int m_zombieNum;
    public float jobTime;
    public float SetJobTime;
    public int jobNum;
    public int soul;
    public int playerStock;

    public int playerHp = 0;
    public float playerWalkSpeed = 5f;
    public float playerDashSpeed = 7f;
    public float playerJumpPower = 1750f;

    public int zonbieHp = 10;
    public int zonbieAttack = 1;
    public float zonbieSpeed = 3f;

    public int vomitHp = 10;
    public int vomitAttack = 3;
    public float vomitSpeed = 5;

    public int dogHp = 5;
    public int dogAttack = 5;
    public float dogSpeed = 10f;

    public int crowHp = 5;
    public int crowAttack = 5;
    public float crowSpeed = 5f;

    public int muscleHp = 10;
    public int muscleAttack = 5;
    public float muscleSpeed = 5f;

}
