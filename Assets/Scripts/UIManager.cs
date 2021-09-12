using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Image blackSreen;
    public float fadeSpeed;
    public bool fadeToBlack, fadeFromBlack;
    public Text healthText;
    public Image healthImage;
    public Text coinText;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeToBlack) {
            blackSreen.color = new Color(blackSreen.color.r, blackSreen.color.g, blackSreen.color.b,
                Mathf.MoveTowards(blackSreen.color.a, 1f, fadeSpeed*Time.deltaTime));

            if (blackSreen.color.a == 1f) {
                fadeToBlack = false;
            }
        }

        if (fadeFromBlack) {
            blackSreen.color = new Color(blackSreen.color.r, blackSreen.color.g, blackSreen.color.b,
                Mathf.MoveTowards(blackSreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (blackSreen.color.a == 0f) {
                fadeFromBlack = false;
            }
        }
    }
}
