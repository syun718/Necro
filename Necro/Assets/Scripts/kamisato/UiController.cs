using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour {

    private Image _life;
    private Image _playerIcon;
    private Image _hijackTime;
    private PlayerController _zombi;
    private PlayerController _dogzombi;
    private GameObject _dogzombie;
    public GameObject m_Player;

    float time;
    private float jobTime;

    public Sprite[] icon_Sprite = new Sprite[0];
    public Sprite[] life_Sprite = new Sprite[0];


    // Use this for initialization
    void Start () {
        _playerIcon = GameObject.Find("Icon").GetComponent<Image>();
        _hijackTime = GameObject.Find("JobTime").GetComponent<Image>();
        //playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        //_zombi = GameObject.Find("Zombie").GetComponent<PlayerController>();
        //_dogzombi = GameObject.Find("Zombie2").GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
        ChangePlayerIcom(gameObject.tag);
    }

    public void ChangePlayerIcom(string tagName)
    {
        switch (tagName)
        {
            case TagName.player:
                _playerIcon.sprite = icon_Sprite[0];
                JobTime();
                break;

            //case TagName.gelozombie:
            //    _playerIcon.sprite = icon_Sprite[1];
            //    JobTime();
            //    Debug.Log("Zombie");
            //    break;
            //case TagName.dogzombie:
                //_playerIcon.sprite = icon_Sprite[2];
                //JobTime();
                //Debug.Log(TagName.gelozombie);
                //break;
        }

    }

    public void Life()
    {

    }

    public void JobTime()
    {
        PlayerData.Instance.jobTime -= Time.deltaTime;
        _hijackTime.fillAmount =  PlayerData.Instance.jobTime / PlayerData.Instance.SetJobTime;
        if(_hijackTime.fillAmount <= 0)
        {
            _playerIcon.sprite = icon_Sprite[0];
            _hijackTime.fillAmount = 1;
        }
    }
}

