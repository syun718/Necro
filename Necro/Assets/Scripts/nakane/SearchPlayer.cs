<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchPlayer : MonoBehaviour {

    void OnTriggerStay2D(Collider2D col)
    {
        NormalZombieMove.EnemyState state = GetComponentInParent<NormalZombieMove>().GetState();

        // プレイヤーを発見
        if (state == NormalZombieMove.EnemyState.Walk)
        {
            if (col.tag == " Man")
            {
                //NormalZombieMove.EnemyState state = GetComponentInParent<NormalZombieMove>().GetState();

                //if(state == NormalZombieMove.EnemyState.Walk)
                //{
                Debug.Log("プレイヤー発見");
                GetComponentInParent<NormalZombieMove>().SetState("chase", col.transform);
                //}
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == " Man")
        {
            Debug.Log("見失う");
            GetComponentInParent<NormalZombieMove>().SetState("walk");
        }
    }

    public enum EnemyState
    {
        Walk,
        Chase
    };

    void Start ()
    {
        	
	}

	void Update ()
    {
		
	}
}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchPlayer : MonoBehaviour {

    void OnTriggerStay2D(Collider2D col)
    {
        NormalZombieMove.EnemyState state = GetComponentInParent<NormalZombieMove>().GetState();

        // プレイヤーを発見
        if (state == NormalZombieMove.EnemyState.Walk)
        {
            if (col.tag == " Man")
            {
                //NormalZombieMove.EnemyState state = GetComponentInParent<NormalZombieMove>().GetState();

                //if(state == NormalZombieMove.EnemyState.Walk)
                //{
                Debug.Log("プレイヤー発見");
                GetComponentInParent<NormalZombieMove>().SetState("chase", col.transform);
                //}
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == " Man")
        {
            Debug.Log("見失う");
            GetComponentInParent<NormalZombieMove>().SetState("walk");
        }
    }

    public enum EnemyState
    {
        Walk,
        Chase
    };

    void Start ()
    {
        	
	}

	void Update ()
    {
		
	}
}
>>>>>>> c85d5b72c8aee1390856735f0a1acd6c4c746bc5
