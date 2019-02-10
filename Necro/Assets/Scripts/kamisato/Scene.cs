using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour {

    PlayerInput m_playerInput;
    // Use this for initialization
    void Start () {
        m_playerInput = GetComponent<PlayerInput>();
    }
	
    

	// Update is called once per frame
	void Update () {
        StartCoroutine("LoadStart");
        //ButtonClicked();
    }

    public void ButtonClicked()
    {
        Debug.Log("aaa");
        if (Input.GetKey("left"))
        {
            Debug.Log("aaa");
            SceneManager.LoadScene("Stage1");
        }

    }
}
