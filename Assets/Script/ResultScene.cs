using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;

public class ResultScene : MonoBehaviour
{
    public Text resultText; // 結果を表示するtext型の変数
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("ResultScene Start!");
        // resultのオブジェクトを探す
        resultText = GameObject.Find("Canvas/Result").GetComponent<Text> ();
        if (QuizScene.isAnswer) {
            resultText.text = "正解!";
        } else {
            resultText.text = "不正解!";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick() {
        Debug.Log("ボタンが押された");
        SceneManager.LoadScene("Quiz");
    }
}
