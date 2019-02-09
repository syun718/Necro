using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeadZone : MonoBehaviour {

    UiController uiController;

	// Use this for initialization
	void Start () {
		uiController = GameObject.Find("UiController").GetComponent<UiController>();
    }
	
	// Update is called once per frame
	void Update () {
        Ded();

    }

    void Ded()
    {
        if(uiController.destroyCount == 3)
        {
            SceneManager.LoadScene("Title");
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            SceneManager.LoadScene("Title");
        }
    }
}
