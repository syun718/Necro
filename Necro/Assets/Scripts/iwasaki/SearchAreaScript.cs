using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchAreaScript : MonoBehaviour {

    public GameObject zombie;
    public int Range_Nom;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == TagName.player)
        {
            if (Range_Nom == 1)
            {
                zombie.gameObject.GetComponent<ZombieController>().Range_Flag1 = true;
            }
            else
            {
                zombie.gameObject.GetComponent<ZombieController>().Range_Flag2 = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("exit");
        if (collision.gameObject.tag == TagName.player)
        {
            if (Range_Nom == 1)
            {
                zombie.gameObject.GetComponent<ZombieController>().Range_Flag1 = false;
            }
            else
            {
                zombie.gameObject.GetComponent<ZombieController>().Range_Flag2 = false;
            }
        }
    }
}
