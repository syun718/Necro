using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour {

    public Image UIobj;
    public float countTime = 5.0f;

    // Update is called once per frame
    void Update()
    {
        Timeat();
    }

    void Timeat()
    {
        UIobj.fillAmount -= 1.0f / countTime * Time.deltaTime;
    }
}
