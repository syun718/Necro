using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour {
    
    private Image _playerIcon;
    private Image _hijackTime;
    private PlayerController _zombi;
    private PlayerController _dogzombi;
    private GameObject _dogzombie;

    public float time = 3.0f;


    public Sprite[] icon_Sprite = new Sprite[0];
    

	// Use this for initialization
	void Start () {
        _playerIcon = GameObject.Find("Icon").GetComponent<Image>();
        _hijackTime = GameObject.Find("JobTime").GetComponent<Image>();
        //playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        _zombi = GameObject.Find("Zombie").GetComponent<PlayerController>();
        _dogzombi = GameObject.Find("Zombie2").GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
        ChangePlayerIcom(gameObject.tag);
    }

    public void ChangePlayerIcom(string tagName)
    {
        switch (tagName)
        {
            case TagName.m_zombie:
                _playerIcon.sprite = icon_Sprite[1];
                JobTime();
                Debug.Log("Zombie");
                break;
            //case TagName.m_dogzombie:
            //    _playerIcon.sprite = icon_Sprite[3];
            //    JobTime();
            //    Debug.Log("DogZombie");
            //    break;
        }

    }

    public void JobTime()
    {
        _hijackTime.fillAmount -= 1.0f / time * Time.deltaTime;
    }
}

