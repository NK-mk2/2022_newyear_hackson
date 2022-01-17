using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;

public class InCorrectScene : MonoBehaviour
{
    public int questionCount; // 解答済みの問題数
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("不正解");
        // 解答した問題数を増やす
        GManager.instance.AddQuestionNum();
        // 回答済みの問題数を取得
        questionCount = GManager.instance.questionNum;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnClick() {
        Debug.Log(questionCount);
        // 10問終わったらEDページへ
        if( questionCount > 10 ) {
            SceneManager.LoadScene("design_Result");
        } else {
            SceneManager.LoadScene("design_Quiz");
        }
    }
}
