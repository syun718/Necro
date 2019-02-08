using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorGimmick : MonoBehaviour {

    public Transform target;
    //public Transform target1;
    private float speed = 100f;
    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        //rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
