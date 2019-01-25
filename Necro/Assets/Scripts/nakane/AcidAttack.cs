using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidAttack : MonoBehaviour {

    public GameObject AcidPrefab;

    // ratio = X座標のどの位置に放物線の山の頂点を持ってくるかを決める割合
    public float acidSpeed, ratio;

    // プレイヤー認識用
    GameObject player;

    void Start ()
    {
        OnStart(GameObject.Find("Zombie").gameObject);
	}

    public void OnStart(GameObject player)
    {
        this.player = player;
        StartCoroutine("acidAttack");
    }

    IEnumerator acidAttack()
    {
        float t = 0.0f;
        float distance = Vector3.Distance(transform.position, player.transform.position);
    
        Vector3 offset = transform.position;
        Vector3 P2 = player.transform.position - offset;
    
        //高度設定
        float angle = 45f;
        float baseRange = 5f;
        float maxAngle = 50f;
    
        angle = angle * distance / baseRange;
        if (angle > maxAngle)
        {
            angle = maxAngle;
        }
    
        float P1x = P2.x * ratio;
    
        // angle * Mathf.Deg2Rad = 角度からラジアンへ変換
        float P1y = Mathf.Sin(angle * Mathf.Deg2Rad) * Mathf.Abs(P1x) / Mathf.Cos(angle * Mathf.Deg2Rad);
        Vector3 P1 = new Vector3(P1x, P1y, 0);
    
        while (t <= 1 && player)
        {
            float Vx = 2 * (1.0f - t) * t * P1.x + Mathf.Pow(t, 2) * P2.x + offset.x;
            float Vy = 2 * (1.0f - t) * t * P1.y + Mathf.Pow(t, 2) * P2.y + offset.y;
            transform.position = new Vector3(Vx, Vy, 0);
        
            t += 1 / distance / acidSpeed * Time.deltaTime;
            
            yield return null;
        }
        Destroy(gameObject);
        //Destroy(acid);

    }
	
}
