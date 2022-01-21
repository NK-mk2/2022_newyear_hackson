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
    [Header("解放されたコレクション")] public List<int> openCollectionNumberList = new List<int>();
    [Header("正解数")] public int correctNum = 0;
    private AudioSource audioSource = null;
    public AudioClip touchBtnSE;



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

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /**
     * 回答した問題数を増やす
     */
    public void AddQuestionNum() {
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

    /// <summary>
    /// SEを鳴らす
    /// </summary>
    public void PlayTouchBtnSE()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(touchBtnSE);
            Debug.Log("音が鳴りました");
        }
        else
        {
            Debug.Log("オーディオソースが設定されていません");
        }
    }

    public void AddCorrectNum() {
        ++correctNum;
    }
}
