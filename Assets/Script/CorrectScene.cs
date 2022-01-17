using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;

public class CorrectScene : MonoBehaviour
{
    //public Text resultText; // 結果を表示するtext型の変数
    public int questionCount; // 解答済みの問題数
    public GameObject getCollection;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("正解");
        getCollection.SetActive(false);
        // 解答した問題数を増やす
        GManager.instance.AddQuestionNum();
        // 回答済みの問題数を取得
        questionCount = GManager.instance.questionNum;

        if (QuizScene.isCollection && string.IsNullOrEmpty(QuizScene.collectionId)) {
            getCollection.SetActive(true);
        }
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
