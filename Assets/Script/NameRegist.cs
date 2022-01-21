using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NameRegist : MonoBehaviour
{
    private bool firstPush = false;
    private bool goNextScene = false;

    //オブジェクトと結びつける
    public InputField inputField;
    [Header("注意文言")] public GameObject text;
    [Header("フェード")] public FadeImage fade;

    void Start()
    {
        //Componentを扱えるようにする
        inputField = inputField.GetComponent<InputField>();

    }

    private void Update()
    {
        if (!goNextScene && fade.IsFadeOutComplete())
        {
            SceneManager.LoadScene("Story");
            goNextScene = true;
        }
    }

    public void InputText()
    {
        //名前を登録
        GManager.instance.player_name = inputField.text;
        text.SetActive(false);
        PressInputComplete();
    }

    //入力完了を押されたら呼ばれる
    public void PressInputComplete()
    {
        Debug.Log("Press InputCoplete!");
        GManager.instance.PlayTouchBtnSE();
        if (string.IsNullOrEmpty(GManager.instance.player_name))
        {
            text.SetActive(true);
        }
        else if (!firstPush)
        {
            Debug.Log("Go Next Scene!");
            fade.StartFadeOut();
            firstPush = true;
        }
        
    }

}