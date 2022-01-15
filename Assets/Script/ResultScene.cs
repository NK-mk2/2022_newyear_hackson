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
    public int questionCount; // 解答済みの問題数
    // Start is called before the first frame update
    void Start()
    {
        // 遷移ボタンのテキストを取得
        Text buttonLabel = GameObject.Find("Canvas/Button").GetComponentInChildren<Text>();
        if ( questionCount >= 10 ) {
            buttonLabel.text = "最終結果！";
        } else {
            buttonLabel.text = "次の問題へ";
        }

        // 解答した問題数を増やす
        GManager.instance.AddQuestionNum();
        // resultのオブジェクトを探す
        resultText = GameObject.Find("Canvas/Result").GetComponent<Text> ();
        if (QuizScene.isAnswer) {
            resultText.text = "正解!";
        } else {
            resultText.text = "不正解!";
        }

        // 回答済みの問題数を取得
        questionCount = GManager.instance.questionNum;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick() {
        // 10問終わったらEDページへ
        if( questionCount >= 10 ) {
            // 開発中は最終結果ページに飛ばす
            //SceneManager.LoadScene("Story");
            SceneManager.LoadScene("Finish");
        } else {
            SceneManager.LoadScene("Quiz");
        }
    }
}
