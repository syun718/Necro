using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_BGM : MonoBehaviour {

    void Awake()
    {
        SoundManager.LoadBGM("Stage1", "Stage1");
    }

	void Start ()
    {
        SoundManager.PlayBGM("Stage1");
	}
	
	void Update ()
    {
		
	}
}
