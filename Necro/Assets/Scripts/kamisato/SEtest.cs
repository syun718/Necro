using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEtest : MonoBehaviour {

    private AudioManager audioManager;

	// Use this for initialization
	void Start () {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("押されたよ");
            audioManager.GeroZombieVoice1();
        }
    }
}
