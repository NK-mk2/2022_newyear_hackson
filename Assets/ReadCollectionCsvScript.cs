using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadCollectionCsvScript : MonoBehaviour
{
    // 流し込む配列
    public CollectionData[] collectionData;
    // Start is called before the first frame update
    void Start()
    {
        // テキストファイルの読み込みを行ってくれるクラス
        TextAsset textasset = new TextAsset();
        //csvファイルを読み込ませる
        // ファイルは「resources」フォルダを作り、そこに入れておく。
        textasset = Resources.Load("collection") as TextAsset;
        // CSVSerializerを用いてcsvファイルを配列に流し込む
        collectionData = CSVSerializer.Deserialize<CollectionData>(textasset.text);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
