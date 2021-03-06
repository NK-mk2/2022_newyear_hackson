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
    public Text collectionTitle; // コレクションのタイトル
    public Image collectionImage;
    private string spritesDirectory = "Sprites/";
    public string ImageName = "itemicon_mini_";
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("正解");
        getCollection.SetActive(false);
        // 解答した問題数を増やす
        GManager.instance.AddQuestionNum();
        // 正解数を増やす
        GManager.instance.AddCorrectNum();

        // 回答済みの問題数を取得
        questionCount = GManager.instance.questionNum;

        if (QuizScene.isCollection && QuizScene.collectionId != 0) {
            getCollection.SetActive(true);
            SetCollectionImage(QuizScene.collectionId);

            Text collectionTitle = GameObject.Find("Canvas/02_Get/popup/popupmid").GetComponentInChildren<Text>();
            collectionTitle.text = "世界に" + QuizScene.collectionText + "の知識が加わった！";

            GManager.instance.openCollectionNumberList.Add(QuizScene.collectionId);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void closeClick() {
        getCollection.SetActive(false);
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

    public void SetCollectionImage(int collectionId)
    {
        collectionImage.sprite = LoadSprite(ImageName + collectionId);
    }

    /**
    * スプライトをファイルから読み出し、インスタンス化する
    */
    private Sprite LoadSprite(string name)
    {
        return Instantiate(Resources.Load<Sprite>(spritesDirectory + name));
    }
}
