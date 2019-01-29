using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SoulShot : MonoBehaviour {

    //public GameObject shotSoul;

    bool shotFlag = true;

    public float soulSpeed = 5f;

    private ChangePlayer changePlayer;
    private Rigidbody2D rb2d;
    private Renderer targetRenderer;


    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        targetRenderer = GetComponent<Renderer>();
        changePlayer = GameObject.Find("PlayerManager").GetComponent<ChangePlayer>();
    }
	
	// Update is called once per frame
	void Update () {
        Shot();
    }


    public void Shot()
    {
        Vector2 bulletMovement = transform.right;
        rb2d.velocity = bulletMovement * soulSpeed;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
        else if(col.gameObject.tag == "Zombie")
        {
            if (PlayerData.Instance.m_zombieNum == 0)
            {
                PlayerData.Instance.m_zombieNum = 1;
                changePlayer.ChangeCharacter(changePlayer.nowChara);
                Destroy(gameObject);
            } else
            {
                PlayerData.Instance.m_zombieNum = 0;
                changePlayer.ChangeCharacter(changePlayer.nowChara);
                Destroy(gameObject);
            }
        }
        
    }


}
