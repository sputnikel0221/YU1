using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;
    Rigidbody2D rigid;
    PlayerMove pm;

    bool upside;

    void Awake()
    {
        player = GameObject.Find("Player");
        rigid = GetComponent<Rigidbody2D>();
        pm = GetComponent<PlayerMove>();
        upside = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (upside)
        {
            rigid.AddForce(Vector2.up * 3.0f, ForceMode2D.Impulse);
            if (Mathf.Abs(rigid.velocity.y) > 5.0f)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, 2 * rigid.velocity.normalized.y);
            }
            Invoke("Origin", 1.2f);
        }
        else
        {
            Vector3 playerPos = player.transform.position;
            transform.position = new Vector3(playerPos.x, playerPos.y, -10);
        }
    }

    //set direction
    public void WatchUp()
    {
        upside = true;
    }

    //reset direction
    void Origin()
    {
        upside = false; 
    }
}


//메인카메라 이동
//https://himbopsa.tistory.com/25