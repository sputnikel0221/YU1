using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMove : MonoBehaviour
{
    GameObject player;
    Animator animator;

    public float dist;
    public float speed;

    void Awake()
    {
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //플레이어의 거리가 가까워졌다면, 날아감
        if (Vector2.Distance(transform.position, player.transform.position) < dist)
        {
            animator.SetBool("isCome", true);
            Follow();
        }

    }

    //플레이어를 계속 따라감
    void Follow()
    {

        if (Vector2.Distance(transform.position, player.transform.position) > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed);
        }

    }

}
