using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;

    public float maxSpeed;
    public float decSpeed;
    public float jumpPower;

    public GameObject secret;
    int isEnterSecret;

    public SwitchWork switchWork;

    public int cameraUp;
    public CameraController cam;

    GameObject mainCamera;

    bool onLedder;
    float playerGravity;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        isEnterSecret = 0;
        onLedder = false;
        playerGravity = rigid.gravityScale;

        mainCamera = GameObject.Find("Main Camera");
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(mainCamera);
    }


    // Update is called once per frame
    void Update()
    {
        //Jump and DoubleJump
        if (Input.GetButtonDown("Jump"))
        {
            if(!animator.GetBool("isJumping")){
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                animator.SetBool("isJumping", true);
            } 
            else if(animator.GetBool("addJump")){
                rigid.AddForce(Vector2.up * jumpPower * 1.2f, ForceMode2D.Impulse);
                animator.SetBool("addJump", false);
            }
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

        //onLedder
        if (onLedder)
        {
            rigid.gravityScale = 0.5f;
            float vy = Input.GetAxisRaw("Vertical");
            rigid.velocity = new Vector2(rigid.velocity.x, vy);
        }
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

        //watch Up , y = 0
        if (Input.GetButton("Up") && rigid.velocity.y == 0)
        {
            cameraUp++;
            // Debug.Log(cameraUp);
            if (cameraUp / 50 > 0.5)
            {
                cam.WatchUp();
                cameraUp = 0;
                // Debug.Log(cameraUp);
            }
        }
        else
        {
            cameraUp = 0;
        }

    }

    //Items or Enemies
    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Cherry":
                animator.SetBool("addJump",true);
                other.gameObject.SetActive(false);
                break;
            case "Gem":
                Animator gemAnimator = other.gameObject.GetComponent<Animator>();
                gemAnimator.SetBool("isGet",true);
                // Invoke("RemoveObj(other)",1);
                StartCoroutine(RemoveObj(other.gameObject));
                break;
        }
    }

    //Remove GameObject after 0.4f
    IEnumerator RemoveObj(GameObject obj){
        yield return new WaitForSeconds(0.4f);
        obj.SetActive(false);
    }


    //Switch and Door
    void OnTriggerStay2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Switch":
                if (Input.GetButton("Select"))
                {
                    switchWork.Work();
                }
                break;
            case "Door":
                if (Input.GetButton("Select"))
                {
                    SceneManager.LoadScene("Tower");
                }
                break;
            case "Ledder":
                onLedder = true;
                break;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //Hiding오브젝트와 충돌시, show seceret room
        if (other.gameObject.tag == "Hiding")
        {
            // Debug.Log("일단 hiding과 충돌한것은 맞음");
            if (isEnterSecret <= 2)
            {
                // Debug.Log("조건에 맞는경우");
                secret.SetActive(false);
                isEnterSecret++;
                // Debug.Log(isEnterSecret);
            }
            else if (isEnterSecret > 2)
            {
                // Debug.Log("조건에 맞지 않는 경우");
                secret.SetActive(true);
                isEnterSecret = 0;
            }
        }

        if (other.gameObject.tag == "Ledder")
        {
            onLedder = false;
            rigid.gravityScale = playerGravity;
        }

    }
}

//2D에서 맵에 낑기는것 방지
//https://junwe99.tistory.com/11

//특정 객체 알아내기
//https://allaboutmakers.tistory.com/32

//비활성화 후 활성화 시키는 법 = 이건 폐기
//https://answers.unity.com/questions/1089570/how-do-you-activate-a-game-object.html

//trigger에 관해.. 왜 다시 부딪혀도 true가 되는거지
//https://answers.unity.com/questions/682296/triggering-only-once.html

//비밀방 해결
//https://forum.unity.com/threads/ontriggerenter-is-called-twice-sometimes.95187/

//Composite
// https://yung.tistory.com/entry/Unity-2D-%EA%B2%8C%EC%9E%84-%EA%B0%9C%EB%B0%9C-%ED%8A%9C%ED%86%A0%EB%A6%AC%EC%96%BC-%EC%A7%80%ED%98%95-%EC%88%98%EC%A0%95%ED%95%98%EA%B8%B0

//camaera객체 가져오기
//https://jhc777.tistory.com/entry/Camera%EB%A5%BC-%EB%B0%94%EA%BF%94%EB%B3%B4%EC%9E%90

//camera 무빙 나중에
//https://timeboxstory.tistory.com/108

//Dont Destroy
//https://chameleonstudio.tistory.com/57

//Invoke는 매개변수 안됨
//https://wonsang98.tistory.com/220

//코루틴
//https://eunjin3786.tistory.com/515

//코루틴 예제
//https://clack.tistory.com/50

//코루틴 해결
//https://coding-of-today.tistory.com/171