using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

// MonoBehaviourを継承することでオブジェクトにコンポーネントとして
// アタッチすることができるようになる
public class Story : MonoBehaviour
{
    // SerializeFieldと書くとprivateなパラメーターでも
    // インスペクター上で値を変更できる
    [SerializeField]
    private Text mainText;
    [SerializeField]
    private Text nameText;
    [SerializeField]
    private float captionSpeed = 0.2f;
    [SerializeField]
    private GameObject nextPageIcon;
    [SerializeField]
    private Image backgroundImage;
    [SerializeField]
    private string spritesDirectory = "Sprites/";
    [SerializeField]
    private GameObject characterImages;
    [SerializeField]
    private string prefabsDirectory = "Prefabs/";
    [SerializeField]
    private string textFile = "Texts/Scenario";
    private const char SEPARATE_MAIN_START = '「';
    private const char SEPARATE_MAIN_END = '」';
    private string _text = "";
    private Queue<char> _charQueue;
    private const char SEPARATE_PAGE = '&';
    private Queue<string> _pageQueue;
    private const char SEPARATE_COMMAND = '!';
    private const char COMMAND_SEPARATE_PARAM = '=';
    private const string COMMAND_BACKGROUND = "background";
    private const string COMMAND_SPRITE = "_sprite";
    private const string COMMAND_COLOR = "_color";
    private const string COMMAND_CHARACTER_IMAGE = "charaimg";
    private const string COMMAND_SIZE = "_size";
    private const string COMMAND_POSITION = "_pos";
    private const string COMMAND_ROTATION = "_rotate";
    private const string CHARACTER_IMAGE_PREFAB = "CharacterImage";
    private List<Image> _charaImageList = new List<Image>();
    private const string COMMAND_CHANGE_SCENE = "scene";

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            OnTouch();
        }
    }

    /**
    * 初期化する
    */
    private void Init()
    {
        _text = LoadTextFile(textFile);
        _pageQueue = SeparateString(_text, SEPARATE_PAGE);
        ShowNextPage();
    }

    /**
    * 文を1文字ごとに区切り、キューに格納したものを返す
    */
    private Queue<char> SeparateString(string str)
    {
        // 文字列をchar型の配列にする = 1文字ごとに区切る
        char[] chars = str.ToCharArray();
        Queue<char> charQueue = new Queue<char>();
        // foreach文で配列charsに格納された文字を全て取り出し
        // キューに加える
        foreach (char c in chars) charQueue.Enqueue(c);
        return charQueue;
    }

    /**
    * 文字を出力する
    */
    private bool OutputChar()
    {
        if (_charQueue.Count <= 0)
        {
            nextPageIcon.SetActive(true);
            return false;
        }
        mainText.text += _charQueue.Dequeue();
        return true;
    }

    /**
    * 文字送りするコルーチン
    */
    private IEnumerator ShowChars(float wait)
    {
        // OutputCharメソッドがfalseを返す(=キューが空になる)までループする
        while (OutputChar())
            // wait秒だけ待機
            yield return new WaitForSeconds(wait);
        // コルーチンを抜け出す
        yield break;
    }

    /**
    * 1行を読み出す
    */
    private void ReadLine(string text)
    {
        // 最初が「!」だったら
        if (text[0].Equals(SEPARATE_COMMAND))
        {
            ReadCommand(text);
            ShowNextPage();
            return;
        }
        string[] ts = text.Split(SEPARATE_MAIN_START);
        string name = ts[0];
        string main = ts[1].Remove(ts[1].LastIndexOf(SEPARATE_MAIN_END));
        nameText.text = name;
        mainText.text = "";
        _charQueue = SeparateString(main);
        StartCoroutine(ShowChars(captionSpeed));
    }

    /**
    * 全文を表示する
    */
    private void OutputAllChar()
    {
        StopCoroutine(ShowChars(captionSpeed));
        while (OutputChar()) ;
        nextPageIcon.SetActive(true);
    }

    /**
    * クリックしたときの処理
    */
    private void OnTouch()
    {
        GManager.instance.PlayTouchBtnSE();
        if (_charQueue.Count > 0) OutputAllChar();
        else
        {
            if (!ShowNextPage())
            {
                Debug.Log("次のページがありません");
                return;
            }
        }
    }

    /**
    * 文字列を指定した区切り文字ごとに区切り、キューに格納したものを返す
    */
    private Queue<string> SeparateString(string str, char sep)
    {
        string[] strs = str.Split(sep);
        Queue<string> queue = new Queue<string>();
        foreach (string l in strs) queue.Enqueue(l);
        return queue;
    }

    
    /**
    * 次のページを表示する
    */
    private bool ShowNextPage()
    {
        if (_pageQueue.Count <= 0) return false;
        // オブジェクトの表示/非表示を設定する
        nextPageIcon.SetActive(false);
        ReadLine(_pageQueue.Dequeue());
        return true;
    }

    /**
    * 背景の設定
    */
    private void SetBackgroundImage(string cmd, string parameter)
    {
        cmd = cmd.Replace(COMMAND_BACKGROUND, "");
        SetImage(cmd, parameter, backgroundImage);
    }

    /**
    * コマンドの読み出し
    */
    private void ReadCommand(string cmdLine)
    {
        cmdLine = cmdLine.Remove(0, 1);
        Queue<string> cmdQueue = SeparateString(cmdLine, SEPARATE_COMMAND);
        foreach (string cmd in cmdQueue)
        {
            string[] cmds = cmd.Split(COMMAND_SEPARATE_PARAM);
            if (cmds[0].Contains(COMMAND_BACKGROUND))
                SetBackgroundImage(cmds[0], cmds[1]);
            if (cmds[0].Contains(COMMAND_CHARACTER_IMAGE))
                SetCharacterImage(cmds[1], cmds[0], cmds[2]);
            if (cmds[0].Contains(COMMAND_CHANGE_SCENE))
                ChangeNextScene(cmds[1]);
        }
    }

    /**
    * スプライトをファイルから読み出し、インスタンス化する
    */
    private Sprite LoadSprite(string name)
    {
        return Instantiate(Resources.Load<Sprite>(spritesDirectory + name));
    }

    /**
    * パラメーターから色を作成する
    */
    private Color ParameterToColor(string parameter)
    {
        string[] ps = parameter.Replace(" ", "").Split(',');
        if (ps.Length > 3)
            return new Color32(byte.Parse(ps[0]), byte.Parse(ps[1]),
                                            byte.Parse(ps[2]), byte.Parse(ps[3]));
        else
            return new Color32(byte.Parse(ps[0]), byte.Parse(ps[1]),
                                            byte.Parse(ps[2]), 255);
    }

    /**
    * 画像の設定
    */
    private void SetImage(string cmd, string parameter, Image image)
    {
        cmd = cmd.Replace(" ", "");
        parameter = parameter.Substring(parameter.IndexOf('"') + 1, parameter.LastIndexOf('"') - parameter.IndexOf('"') - 1);
        switch (cmd)
        {
            case COMMAND_SPRITE:
                image.sprite = LoadSprite(parameter);
                break;
            case COMMAND_COLOR:
                image.color = ParameterToColor(parameter);
                break;
            case COMMAND_SIZE:
                image.GetComponent<RectTransform>().sizeDelta = ParameterToVector3(parameter);
                break;
            case COMMAND_POSITION:
                image.GetComponent<RectTransform>().anchoredPosition = ParameterToVector3(parameter);
                break;
            case COMMAND_ROTATION:
                image.GetComponent<RectTransform>().eulerAngles = ParameterToVector3(parameter);
                break;
        }
    }

    /**
    * 立ち絵の設定
    */
    private void SetCharacterImage(string name, string cmd, string parameter)
    {
        cmd = cmd.Replace(COMMAND_CHARACTER_IMAGE, "");
        name = name.Substring(name.IndexOf('"') + 1, name.LastIndexOf('"') - name.IndexOf('"') - 1);
        Image image = _charaImageList.Find(n => n.name == name);
        if (image == null)
        {
            image = Instantiate(Resources.Load<Image>(prefabsDirectory + CHARACTER_IMAGE_PREFAB), characterImages.transform);
            image.name = name;
            _charaImageList.Add(image);
        }
        SetImage(cmd, parameter, image);
    }

    /**
    * パラメーターからベクトルを取得する
    */
    private Vector3 ParameterToVector3(string parameter)
    {
        string[] ps = parameter.Replace(" ", "").Split(',');
        return new Vector3(float.Parse(ps[0]), float.Parse(ps[1]), float.Parse(ps[2]));
    }

    /**
    * テキストファイルを読み込む
    */
    private string LoadTextFile(string fname)
    {
        TextAsset textasset = Resources.Load<TextAsset>(fname);
        return textasset.text.Replace("\n", "").Replace("\r", "");
    }

    /**
    * 対応するシーンに切り替える
    */
    private void ChangeNextScene(string parameter)
    {
        parameter = parameter.Substring(parameter.IndexOf('"') + 1, parameter.LastIndexOf('"') - parameter.IndexOf('"') - 1);
        SceneManager.LoadScene(parameter);
    }
}