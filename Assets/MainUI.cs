using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public Text UIPoint;

    void Update(){
        if(GameObject.Find("Score")){
            GameObject score = GameObject.Find("Score");
            
        }
    }

    public void OnClicKPlay(){
        SceneManager.LoadScene("SampleScene");
    }
    public void OnClicKTIM(){
        Debug.Log("팀버튼");
    }
    public void OnClicKQuit(){
         Debug.Log("Quit");
        Application.Quit();
    }

}