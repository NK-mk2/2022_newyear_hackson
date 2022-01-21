using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;

public class FinishScene : MonoBehaviour
{
    TextAsset quizFile; // クイズのCSVファイル
    public static List<string[]> quizDatas = new List<string[]>(); // クイズCSVの中身を入れるリスト
    public static List<int> qnumber = new List<int>(); // 出題番号リスト
    public int csvrow; // CSVファイルの行数
    private int k = 0; // 配列の変数
    private int qransu = 0; // 出題する問題の行
    public Text scoreText; // スコアを表示するtext型の変数
    public Text correctText; // スコアを表示するtext型の変数
    public GameObject Incorrect;
    public GameObject Advice;
    public int questionCount; // 解答済みの問題数

    public int correntCount; // 解答済みの問題数
    // Start is called before the first frame update
    void Start()
    {
        //Incorrect.SetActive(false);
        Advice.SetActive(false);
        // 各UIにセットするパラメータを取得してcompornentに設定する
        // スコア
        scoreText = GameObject.Find("Canvas/01_Result/Score/ScoreText").GetComponent<Text>();
        scoreText.text = GManager.instance.score.ToString("F0");

        // 正答率
        correctText = GameObject.Find("Canvas/01_Result/WorldScore/ScoreText").GetComponent<Text>();
        correctText.text = GManager.instance.correctNum + "%";

        if (PlayerPrefs.HasKey("missrow")) {
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
            csvrow = 0; // 変数初期化

            qransu = qnumber[PlayerPrefs.GetInt("missrow", 0)];
            QuestionLabelSet();
            AnswerLabelSet();

            Incorrect.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick() {
        SceneManager.LoadScene("Start");
    }

    public void closeClick() {
        Incorrect.SetActive(false);
    }

    /**
    * 問題文のセット
    */
    private void QuestionLabelSet() {
        quizDatas[k] = quizDatas[qransu]; // CSVの"qransu"行目の問題を取得
        Text qLabel = GameObject.Find("Canvas/02_Review_Incorrect/popup/popupmid").GetComponentInChildren<Text>();
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
            Text answerLabel = GameObject.Find("Canvas/02_Review_Incorrect/Button" + i).GetComponentInChildren<Text>();
            answerLabel.text = array[i-1];
        }
    }
}
