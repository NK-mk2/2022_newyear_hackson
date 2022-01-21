using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionDetail : MonoBehaviour
{
    public GameObject CollectionDetailArea;
    public Text CollectionName;
    public Text CollectionDetailText;
    public Image CollectionImage;
    [SerializeField]
    private string spritesDirectory = "Sprites/";
    public string Name = "";
    public string Detail = "";
    public string ImageName = "itemicon_mini_0";
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowDetail()
    {
        CollectionDetailArea.SetActive(true);
        CollectionName = CollectionDetailArea.transform.Find("header").transform.Find("CollectionName").GetComponent<Text>();
        CollectionName.text = Name;
        CollectionDetailText = CollectionDetailArea.transform.Find("CollectionDetailText").GetComponent<Text>();
        CollectionDetailText.text = Detail;
        CollectionImage = CollectionDetailArea.transform.Find("CollectionImage").GetComponent<Image>();
        CollectionImage.sprite = LoadSprite(ImageName);
        GManager.instance.PlayTouchBtnSE();
    }

    /**
    * スプライトをファイルから読み出し、インスタンス化する
    */
    private Sprite LoadSprite(string name)
    {
        return Instantiate(Resources.Load<Sprite>(spritesDirectory + name));
    }
}
