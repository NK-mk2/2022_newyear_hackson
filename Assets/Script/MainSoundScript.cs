using UnityEngine;
using System.Collections;

public class MainSoundScript : MonoBehaviour
{
    public static MainSoundScript instance = null;
    public bool DontDestroyEnabled = true;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

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