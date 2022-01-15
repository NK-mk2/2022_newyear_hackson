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
    // Start is called before the first frame update
    void Start()
    {
        // 各UIにセットするパラメータを取得してcompornentに設定する
        // スコア
        Debug.Log(GManager.instance.score);
        scoreText = GameObject.Find("Canvas/Score/ScoreText").GetComponent<Text>();
        scoreText.text = GManager.instance.score.ToString("F0");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
