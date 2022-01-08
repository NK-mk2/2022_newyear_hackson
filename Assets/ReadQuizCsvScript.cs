using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadQuizCsvScript : MonoBehaviour
{
    // 流し込む配列
    public QuizData[] quizData;
    // Start is called before the first frame update
    void Start()
    {
        // テキストファイルの読み込みを行ってくれるクラス
        TextAsset textasset = new TextAsset();
        //csvファイルを読み込ませる
        // ファイルは「resources」フォルダを作り、そこに入れておく。
        textasset = Resources.Load("quiz") as TextAsset;
        // CSVSerializerを用いてcsvファイルを配列に流し込む
        quizData = CSVSerializer.Deserialize<QuizData>(textasset.text);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
