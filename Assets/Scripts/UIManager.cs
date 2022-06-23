using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // [SerializeField]
    // private Text[] txtx; 
    // [SerializeField]
    // private Text scoreTxt;
    [SerializeField]
    private Text gameOver;
    void Start()
    {
        // scoreTxt.text = "Score: " + 0;
        gameOver.text = "";
    }

    // public void updateScore(int val){
    //     scoreTxt.text = "Score: " + val.ToString();
    // }
    public void showtext(float lif){
        if(lif < 1){
            gameOver.text = "GAME OVER!!";
            // Time.timeScale = 0f;
        }
    }
}
