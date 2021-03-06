using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;

public class Collection : MonoBehaviour
{
    [Header("γγ§γΌγ")] public FadeImage fade;
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
                    Detail.SetCollectionImage();
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
        // γγ­γΉγγγ‘γ€γ«γ?θͺ­γΏθΎΌγΏγθ‘γ£γ¦γγγγ―γ©γΉ
        TextAsset textasset = new TextAsset();
        //csvγγ‘γ€γ«γθͺ­γΏθΎΌγΎγγ
        // γγ‘γ€γ«γ―γresourcesγγγ©γ«γγδ½γγγγγ«ε₯γγ¦γγγ
        textasset = Resources.Load("collection") as TextAsset;
        // CSVSerializerγη¨γγ¦csvγγ‘γ€γ«γιεγ«ζ΅γθΎΌγ
        StringReader reader = new StringReader(textasset.text);
        // reader.Peekγ-1γ«γͺγγΎγ§
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine(); // 1θ‘γγ€θͺ­γΏθΎΌγΏ
            collectionDatas.Add(line.Split(',')); // ,εΊεγγ§γͺγΉγγ«θΏ½ε 
            csvrow++; // CSVγγ‘γ€γ«γ?θ‘ζ°γγ«γ¦γ³γ
        }
    }

    public void CloseDetail()
    {
        GManager.instance.PlayTouchBtnSE();
        CollectionDetailArea.SetActive(false);
    }

    //γΉγΏγΌγγγΏγ³γζΌγγγγεΌγ°γγ
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