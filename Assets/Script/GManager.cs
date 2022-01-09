using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ゲーム全体をコントロールするスクリプト
public class GManager : MonoBehaviour
{
    public static GManager instance = null;

    [Header("名前")] public string player_name = null;


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
}
