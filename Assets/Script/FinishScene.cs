using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;

public class FinishScene : MonoBehaviour
{
    public Text scoreText; // スコアを表示するtext型の変数
    public Text correctText; // スコアを表示するtext型の変数
    public GameObject Incorrect;
    public GameObject Advice;
    public int questionCount; // 解答済みの問題数

    public int correntCount; // 解答済みの問題数
    // Start is called before the first frame update
    void Start()
    {
        // 各UIにセットするパラメータを取得してcompornentに設定する
        // スコア
        scoreText = GameObject.Find("Canvas/01_Result/Score/ScoreText").GetComponent<Text>();
        scoreText.text = GManager.instance.score.ToString("F0");

        // 正答率
        correctText = GameObject.Find("Canvas/01_Result/WorldScore/ScoreText").GetComponent<Text>();
        correctText.text = GManager.instance.correctNum + "%";

        Incorrect.SetActive(false);
        Advice.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick() {
        SceneManager.LoadScene("Start");
    }
}
