using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;

public class Collection : MonoBehaviour
{
    [Header("フェード")] public FadeImage fade;
    public int csvrow; 
    public static List<string[]> collectionDatas = new List<string[]>();
    public int totalCollectionCount;
    public int collectionNum = 1;
    public GameObject collectionObjPrefab;
    public GameObject unknownCollectionObjPrefab;
    public GameObject collectionsObj;
    public GameObject CollectionDetailArea;
    private bool firstPush = false;
    private bool goStartScene = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!goStartScene && fade.IsFadeOutComplete())
        {
            SceneManager.LoadScene("Start");
            goStartScene = true;
        }
    }

    void Awake()
    {
        ReadCollectionCsv();
        totalCollectionCount = collectionDatas.Count;
        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                if(GManager.instance.openCollectionNumberList.Contains(collectionNum))
                {
                    GameObject g = Instantiate(collectionObjPrefab, collectionsObj.transform);
                    g.transform.position = new Vector3((-1.4f + (1.4f * x)), (2.7f + (-1.3f * y)), 0);
                    CollectionDetail Detail = g.GetComponent<CollectionDetail>();
                    Detail.ImageName += collectionDatas[collectionNum][0];
                    Detail.Name = collectionDatas[collectionNum][1];
                    Detail.Detail = collectionDatas[collectionNum][2];
                    Detail.CollectionDetailArea = CollectionDetailArea;
                }
                else
                {
                    GameObject g = Instantiate(unknownCollectionObjPrefab, collectionsObj.transform);
                    g.transform.position = new Vector3((-1.4f + (1.4f * x)), (2.7f + (-1.3f * y)), 0);
                }

                collectionNum++;
                if (totalCollectionCount <= collectionNum)
                {
                    return;
                }
            }
        }
    }

    void ReadCollectionCsv()
    {
        // テキストファイルの読み込みを行ってくれるクラス
        TextAsset textasset = new TextAsset();
        //csvファイルを読み込ませる
        // ファイルは「resources」フォルダを作り、そこに入れておく。
        textasset = Resources.Load("collection") as TextAsset;
        // CSVSerializerを用いてcsvファイルを配列に流し込む
        StringReader reader = new StringReader(textasset.text);
        // reader.Peekが-1になるまで
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine(); // 1行ずつ読み込み
            collectionDatas.Add(line.Split(',')); // ,区切りでリストに追加
            csvrow++; // CSVファイルの行数をカウント
        }
    }

    public void CloseDetail()
    {
        GManager.instance.PlayTouchBtnSE();
        CollectionDetailArea.SetActive(false);
    }

    //スタートボタンを押されたら呼ばれる
    public void BackToStart()
    {
        Debug.Log("Press Start!");
        if (!firstPush)
        {
            GManager.instance.PlayTouchBtnSE();
            Debug.Log("Go Next Scene!");
            fade.StartFadeOut();
            firstPush = true;
        }
    }
}