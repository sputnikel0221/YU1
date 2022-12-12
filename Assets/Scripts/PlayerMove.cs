using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;

    public float maxSpeed;
    public float decSpeed;
    public float jumpPower;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        //Jump
        if (Input.GetButtonDown("Jump") && !animator.GetBool("isJumping"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
        }

        //decSpeed
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.x * decSpeed, rigid.velocity.y);
        }

        //flip X
        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }

        //change to Idle
        if (Mathf.Abs(rigid.velocity.x) < 0.3f)
        {
            animator.SetBool("isRunning", false);
        }
        else animator.SetBool("isRunning", true);



    }

    void FixedUpdate()
    {

        //Horizontal Move
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
        if (Mathf.Abs(rigid.velocity.x) > maxSpeed)
        {
            rigid.velocity = new Vector2(maxSpeed * rigid.velocity.normalized.x, rigid.velocity.y);
        }

        //isJumping?
        Debug.DrawRay(rigid.position, Vector2.down, new Color(0, 1, 0));
        if (rigid.velocity.y < 0)
        {
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector2.down, 1, LayerMask.GetMask("Ground"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.25f)
                {
                    animator.SetBool("isJumping", false);
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //만약 부딪힌게 Disappear Tag시, 사라짐
        if (other.gameObject.tag == "Hiding")
        {
            GameObject[] secret = GameObject.FindGameObjectsWithTag("Disappear");
            foreach (GameObject obj in secret)
            {
                obj.SetActive(false);

            }

        }
    }
}

//2D에서 맵에 낑기는것 방지
//https://junwe99.tistory.com/11

//특정 객체 알아내기
//https://allaboutmakers.tistory.com/32

//비활성화 후 활성화 시키는 법
//https://answers.unity.com/questions/1089570/how-do-you-activate-a-game-object.html

//trigger에 관해.. 왜 다시 부딪혀도 true가 되는거지
//https://answers.unity.com/questions/682296/triggering-only-once.html