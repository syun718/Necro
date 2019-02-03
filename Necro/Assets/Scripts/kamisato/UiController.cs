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
    //public GameObject m_Player;

    float time;
    private float jobTime;

    public Sprite[] icon_Sprite = new Sprite[0];
    public Sprite[] life_Sprite = new Sprite[0];


    // Use this for initialization
    void Start () {
        _playerIcon = GameObject.Find("Icon").GetComponent<Image>();
        _hijackTime = GameObject.Find("JobTime").GetComponent<Image>();
        _zombi = GameObject.FindGameObjectWithTag("Zombie").GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
        ChangePlayerIcom();
    }

    public void ChangePlayerIcom()
    {
        switch (_zombi.m_job)
        {
            
            case PlayerController.PlayerJob.GeroZomie:
                _playerIcon.sprite = icon_Sprite[1];
                JobTime();
                break;
            case PlayerController.PlayerJob.Dogzombie:
                _playerIcon.sprite = icon_Sprite[2];
                JobTime();
                break;
            case PlayerController.PlayerJob.PowerZombie:
                _playerIcon.sprite = icon_Sprite[3];
                JobTime();
                break;
            case PlayerController.PlayerJob.BirdZombie:
                _playerIcon.sprite = icon_Sprite[4];
                JobTime();
                break;
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

