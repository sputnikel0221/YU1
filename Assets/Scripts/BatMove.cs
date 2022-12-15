using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMove : MonoBehaviour
{
    GameObject player;
    Animator animator;
    Rigidbody2D rigid;

    public float dist;
    public float speed;

    void Awake()
    {
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //플레이어의 거리가 가까워졌다면, 날아감
        if (Vector2.Distance(transform.position, player.transform.position) < dist)
        {
            animator.SetBool("isCome", true);
            StartCoroutine(ForeverFollow(player));
        }

    }

    IEnumerator ForeverFollow(GameObject targetObject)
    {
        while (true)
        {
            //타겟-자신의 위치를 벡터화
            Vector3 dir = (targetObject.transform.position - this.transform.position).normalized;

            float vx = dir.x * speed;
            float vy = dir.y * speed;

            rigid.velocity = new Vector2(vx, vy);

            //(vx < 0)이 참이면 true가 되고, flipX되므로 반전을 하게 됨. 
            //캐릭터 이미지가 기본으로 오른쪽을 보므로 왼쪽을 보게 됨 .
            this.GetComponent<SpriteRenderer>().flipX = (vx < 0);

            yield return 1.0f;
        }
    }
}
