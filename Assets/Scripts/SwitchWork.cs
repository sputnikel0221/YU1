using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWork : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Sprite worked;
    GameObject door;
    
    
    void Awake()
    {
        door = GameObject.FindGameObjectWithTag("Door");
        door.SetActive(false);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Work(){
        spriteRenderer.sprite = worked;
        door.SetActive(true);
    }
}

//오브젝트 찾는법
//https://funfunhanblog.tistory.com/21