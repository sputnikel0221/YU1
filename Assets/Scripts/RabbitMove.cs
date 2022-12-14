using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitMove : MonoBehaviour
{
    GameObject player;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;

    //diffX를 조절할 계수 vx, 점프 파워를 맡는 jp
    public float vx;
    public float jp;

    //jump횟수를 세기 위한 count
    public int count;

    //diff 크기의 max값을 위한 변수
    public float maxDiff;

    void Awake()
    {
        player = GameObject.Find("Player");
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        count = 0;

        StartCoroutine(HopCycle(player));
    }

    //HopCycle은 2초를 주기로 실행이 되며, 1번의 HopCycle에 3번을 뛰도록 해보았다.
    IEnumerator HopCycle(GameObject player)
    {
        while (true)
        {
            //1사이클에 3번 뛴다.
            if (count < 3)
            {
                animator.SetBool("isRest",false);

                //diffx라는 토끼본인과 플레이어의 x값의 차이를 구한다.
                float diffX = player.transform.position.x - transform.position.x;

                //flipX
                spriteRenderer.flipX = diffX * vx < 0;

                //diffX범위정하기 (-maxDiff < diffX < maxDiff)
                if (diffX > maxDiff)
                {
                    diffX = maxDiff;
                }
                else if (diffX < -maxDiff)
                {
                    diffX = -maxDiff;
                }

                //실제로 점프
                rigid.AddForce(new Vector3(diffX * vx, jp, 0), ForceMode2D.Impulse);

                count++;
                yield return new WaitForSeconds(1.0f);
            }
            //3번 다 뛰었다면, 쉬고 다음 사이클 시작
            else
            {
                animator.SetBool("isRest",true);
                //토끼의 재정비 시간..
                if(player.transform.position.x > 0){
                    rigid.AddForce(Vector2.left * 1.2f, ForceMode2D.Impulse);
                } else  rigid.AddForce(Vector2.right *1.2f, ForceMode2D.Impulse);

                count = 0;
                yield return new WaitForSeconds(2.0f);
            }

        }
    }
}
