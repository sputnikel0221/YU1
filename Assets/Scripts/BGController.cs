using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGController : MonoBehaviour
{
    GameObject player;
    public float maxHeight;
    public float minHeight;
    public float moveVertical;

    void Awake()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        Vector3 playerPos = player.transform.position;
        if(playerPos.y<=minHeight){
            transform.position = new Vector3(playerPos.x, minHeight+moveVertical, 10);
        } else if(playerPos.y>maxHeight){
            transform.position = new Vector3(playerPos.x, maxHeight+moveVertical, 10);
        } else {
            transform.position = new Vector3(playerPos.x, playerPos.y+moveVertical, 10);
        } 
    }
}
