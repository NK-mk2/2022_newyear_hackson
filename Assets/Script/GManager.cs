using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ゲーム全体をコントロールするスクリプト
public class GManager : MonoBehaviour
{
    public static GManager instance = null;

    [Header("名前")] public string player_name = null;
    [Header("スコア")] public int score = 0;
    [Header("問題数")] public int questionNum = 1;
    [Header("解放されたコレクション")] public int[] openCollectionNumberList = { 1 };



    // シーンに一つしか作成されないように
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    /**
     * 回答した問題数を増やす
     */
    public void AddQuestionNum() {
        Debug.Log("add");
        if ( questionNum < 11 ) {
            ++questionNum;
        }
    }

    /**
     *
     */
    public void AddScore(int nowScore) {
        score += nowScore;
    }
}
