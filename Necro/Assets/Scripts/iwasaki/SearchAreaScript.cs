using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchAreaScript : MonoBehaviour {

    public GameObject zombie;

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("sss");
        if (collision.gameObject.tag == TagName.player)
        {
            Debug.Log("sss");
            zombie.gameObject.GetComponent<ZombieController>().Range_Flag = true;
        }
    }
}
