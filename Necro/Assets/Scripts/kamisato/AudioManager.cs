using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    static public AudioManager instance;

    public AudioClip TitleBGM;
    public AudioClip SelectBGM;
    public AudioClip StageBGM1;
    public AudioClip StageBGM2;
    public AudioClip StageBGM3;
    public AudioClip Dash1;
    public AudioClip Dash2;
    public AudioClip Jump;
    public AudioClip Landing;
    public AudioClip Attack1;
    public AudioClip Attack2;
    public AudioClip GeroAttack;

    
    private AudioSource audioSource;
    private AudioSource _geroZombie;
    private AudioSource _powerZombie;
    private AudioSource _crowZombie;
    private AudioSource _dogzombie;

    public int BGMFlg = 1;

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = gameObject.GetComponent<AudioSource>();
        _geroZombie = GameObject.FindGameObjectWithTag("Gero").GetComponent<AudioSource>();
        _powerZombie = GameObject.FindGameObjectWithTag("GeroZoｍbie").GetComponent<AudioSource>();
        _crowZombie = GameObject.FindGameObjectWithTag("GeroZoｍbie").GetComponent<AudioSource>();
        _dogzombie = GameObject.FindGameObjectWithTag("GeroZoｍbie").GetComponent<AudioSource>();
    }

        public void StartBGM1()
    {
        audioSource.clip = TitleBGM;
        audioSource.Play();
        BGMFlg = 1;
    }

    public void StartBGM2()
    {
        audioSource.clip = SelectBGM;
        audioSource.Play();
        BGMFlg = 2;
    }

    public void StartBGM3()
    {
        audioSource.clip = StageBGM1;
        audioSource.Play();
        BGMFlg = 3;
    }

    public void StartBGM4()
    {
        audioSource.clip = StageBGM2;
        audioSource.Play();
        BGMFlg = 4;
    }

    public void StartBGM5()
    {
        audioSource.clip = StageBGM3;
        audioSource.Play();
        BGMFlg = 5;
    }

    public int GetBGMFlg()
    {
        return BGMFlg;
    }

    public void StopBGM()
    {
        audioSource.Stop();
    }

    public void DashSE1()
    {
        audioSource.PlayOneShot(Dash1);
    }

    public void DashSE2()
    {
        audioSource.PlayOneShot(Dash2);
    }

    public void JumpSe()
    {
        audioSource.PlayOneShot(Jump);
    }

    public void LandingSE()
    {
        audioSource.PlayOneShot(Landing);
    }

    public void AttackSE1()
    {
        audioSource.PlayOneShot(Attack1);
    }

    public void AttackSE2()
    {
        audioSource.PlayOneShot(Attack2);
    }


    public void GeroZombieVoice1()
    {
        _geroZombie.clip = GeroAttack;
        _geroZombie.Play();
    }

    //public void SESound()
    //{
    //    for(int i = 0; i <= 6; i++)
    //    {
    //        SE[i] = 
    //    }
    //}

}
