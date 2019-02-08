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
    private PlayerController zombie;
    private PlayerController player;
    private UiController uiController;


    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        targetRenderer = GetComponent<Renderer>();
        zombie = GameObject.FindGameObjectWithTag("Zombie").GetComponent<PlayerController>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        uiController = GameObject.Find("UiController").GetComponent<UiController>();
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
        if(player.m_ShotSoul == false)
        {
            player.m_ShotSoul = true;
        }
        else if(zombie.m_ShotSoul == false)
        {
            zombie.m_ShotSoul = true;
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
            if (PlayerData.Instance.m_zombieNum == 0)
            {
                PlayerData.Instance.m_zombieNum = 1;
                uiController.destroyCount = 0;
                if (uiController.destroyCount == 0)
                {
                    uiController.life[0].SetActive(true);
                    uiController.life[1].SetActive(true);
                    uiController.life[2].SetActive(true);
                }
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
