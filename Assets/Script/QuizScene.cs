using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Linq;

public class QuizScene : MonoBehaviour
{
    TextAsset quizFile; // クイズのCSVファイル
    TextAsset choiceFile; // 選択肢のCSVファイル
    public static List<string[]> quizDatas = new List<string[]>(); // クイズCSVの中身を入れるリスト
    public static List<string[]> choinceDatas = new List<string[]>(); // 選択肢CSVの中身を入れるリスト
    public static List<int> qnumber = new List<int>(); // 出題番号リスト
    public int csvrow; // CSVファイルの行数
    public static string answer; // クイズの答え
    private int k = 0; // 配列の変数
    private int qransu = 0; // 出題する問題の行
    public float countdowntime = 10.0f; // 制限時間
    public Text timeText; // 時間を表示するtext型の変数
    public static bool isAnswer = false;

    void Start() {
        timeText = GameObject.Find("Canvas/Timelimit").GetComponent<Text> (); // 時間制限のテキストを取得
        // 問題の生成
        CreateQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        // 制限時間のカウントダウン
        Countdown();
    }

    private void CreateQuestion() {
        // 回答済みの問題数を取得
        int questionCount = GManager.instance.questionNum;
        Debug.Log(questionCount);
        if ( questionCount == 0 ) {
            Debug.Log("CSV作る");
            quizFile = Resources.Load("question") as TextAsset; // Resource配下のCSV読み込み
            StringReader reader = new StringReader(quizFile.text);
            // reader.Peekが-1になるまで
            while (reader.Peek() != -1){
                string line = reader.ReadLine(); // 1行ずつ読み込み
                quizDatas.Add(line.Split(',')); // ,区切りでリストに追加
                csvrow++; // CSVファイルの行数をカウント
            }
            for (int j = 1; j <= csvrow-1; j++) {
                qnumber.Add(j); // CSVファイルの行をリストに追加(1行目は除外)
            }
            qnumber = qnumber.OrderBy( a => System.Guid.NewGuid () ).ToList(); // 読み込んだリストをシャッフル
            csvrow = 0; // 変数初期化
        }

        qransu = qnumber[questionCount]; // シャッフルされたリストから問題を取得
        QuestionLabelSet();
        AnswerLabelSet();
    }

    /**
    * 問題文のセット
    */
    private void QuestionLabelSet() {
        quizDatas[k] = quizDatas[qransu]; // CSVの"qransu"行目の問題を取得
        Text qLabel = GameObject.Find("Canvas/Question").GetComponent<Text>();
        qLabel.text = quizDatas[k][3];
    }

    /**
    * 解答をボタンにセット
    */
    private void AnswerLabelSet() {
        // 解答文面の作成
        string[] array = new string[] { quizDatas[k][4], quizDatas[k][5], quizDatas[k][6], quizDatas[k][7] };
        array = array.OrderBy( x => System.Guid.NewGuid () ).ToArray(); // 解答リストをシャッフル
        // ボタンに代入
        for (int i = 1; i <= 4; i++) {
            Text answerLabel = GameObject.Find("Canvas/Button" + i).GetComponentInChildren<Text>();
            answerLabel.text = array[i-1];
            answer = quizDatas[k][8];
        }
    }


    private void Countdown()
    {
        countdowntime -= Time.deltaTime; //時間をカウントダウンする
        timeText.text = countdowntime.ToString("F1") + "秒"; //時間を表示する
        //countdownが0以下になったとき
        if (countdowntime <= 0)
        {
            //string answerText = answerget();
            isAnswer = false;
            SceneManager.LoadScene("Result");
        }
    }

    /**
    * 解答ボタンが押された時
    */
    public void OnClick() {
        //選択したボタンのテキストラベルを取得する
        Text selectedBtn = this.GetComponentInChildren<Text> ();
        // ボタンを押したら正誤判定をして結果ページへ
        if ( selectedBtn.text == answer ) {
            isAnswer = true;
            // 正解の場合のみスコアを追加する
            int addScore = (int)Math.Round(countdowntime) * 100;
            GManager.instance.AddScore(addScore);
            Debug.Log(GManager.instance.score);
            SceneManager.LoadScene("Result");
        } else {
            isAnswer = false;
            SceneManager.LoadScene("Result");
        }
    }
}
