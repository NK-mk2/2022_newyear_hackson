using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StartScene : MonoBehaviour
{
    [Header("フェード")] public FadeImage fade;
    [Header("スコア")] public Text HiScore = null;
    private bool firstPush = false;
    private bool goRegistName = false;
    private bool goCollection = false;
    private bool goNextScene = false;

    public void Start()
    {
        HiScore.text = "HI - SCORE " + GManager.instance.score.ToString();
    }
    //スタートボタンを押されたら呼ばれる
    public void PressStart()
    {
        Debug.Log("Press Start!");
        if (!firstPush)
        {
            GManager.instance.PlayTouchBtnSE();
            Debug.Log("Go Next Scene!");
            fade.StartFadeOut();
            firstPush = true;
            goRegistName = true;
        }
    }

    //スタートボタンを押されたら呼ばれる
    public void PressCollection()
    {
        Debug.Log("Press Start!");
        if (!firstPush)
        {
            GManager.instance.PlayTouchBtnSE();
            Debug.Log("Go Next Scene!");
            fade.StartFadeOut();
            firstPush = true;
            goCollection = true;
        }
    }

    private void Update()
    {
        if (!goNextScene && fade.IsFadeOutComplete())
        {
            if(goRegistName)
            {
                SceneManager.LoadScene("RegistName");
                goNextScene = true;
            }
            else if (goCollection)
            {
                SceneManager.LoadScene("Collection");
                goNextScene = true;
            }
        }
    }
}