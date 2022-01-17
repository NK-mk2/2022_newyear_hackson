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
    public GameObject Incorrect;
    public GameObject Advice;
    // Start is called before the first frame update
    void Start()
    {
        // 各UIにセットするパラメータを取得してcompornentに設定する
        // スコア
        scoreText = GameObject.Find("Canvas/01_Result/Score/ScoreText").GetComponent<Text>();
        scoreText.text = GManager.instance.score.ToString("F0");

        Incorrect.SetActive(false);
        Advice.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
