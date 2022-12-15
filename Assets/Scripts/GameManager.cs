using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    //체력, 점수, (시간), 더블점프 구현을 위한 변수
    public int hp;
    public int totalPoint;

    public bool showFrog;
    
    //UI를 담을 객체 공간 마련
    public Image[] UIhealth;
    public Text UIPoint;
    public Text UIPointShadow;
    public Text UITime;
    public GameObject UIFrog;

    //player
    public PlayerMove player;

    Animator frogAnimator;

    void Awake() {
        hp = 3;
        frogAnimator = UIFrog.GetComponent<Animator>();
        UIFrog.SetActive(false);
    }


    //Score가 바뀐다면, 알아서 UI객체로 update
    //시간은 알아서 UI객체로 update
    //더블점프를 위한 flag가 참으로 동기화된다면, Frog를 보여준다.
    void Update() {
        UIPoint.text = totalPoint.ToString("D3");
        UIPointShadow.text = totalPoint.ToString("D3");
        UITime.text = ((int)Time.time).ToString("D2");

        if(showFrog){
            UIFrog.SetActive(true);
            frogAnimator.keepAnimatorControllerStateOnDisable  = true;
        } else {
            UIFrog.SetActive(false);
        }

    }

    public void HPDown(){
        UIhealth[hp-1].enabled = false;
        hp--;
        if(hp==0){
            //game Over
        }
    }

    public void AddScore(){
        totalPoint += 100;
    }

    public void DoubleJump(bool flag){
        Debug.Log(flag);
        showFrog = flag; 
    }

    public void EndGame(){
        
    }

}
    
//UI해상도
//https://maeum123.tistory.com/140
