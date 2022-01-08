using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadChoiceCsvScript : MonoBehaviour
{
    // 流し込む配列
    public ChoiceData[] choiceData;
    // Start is called before the first frame update
    void Start()
    {
        // テキストファイルの読み込みを行ってくれるクラス
        TextAsset textasset = new TextAsset();
        //csvファイルを読み込ませる
        // ファイルは「resources」フォルダを作り、そこに入れておく。
        textasset = Resources.Load("choice") as TextAsset;
        // CSVSerializerを用いてcsvファイルを配列に流し込む
        choiceData = CSVSerializer.Deserialize<ChoiceData>(textasset.text);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
