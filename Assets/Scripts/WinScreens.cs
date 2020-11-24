using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WinScreens : MonoBehaviour
{
    private Collider2D col;
    private UIHandler ui;
    public Sprite winScreen1;
    public Sprite winScreen2;
    public Image wScreen;
    public GameObject wSO;
    public TextMeshProUGUI wsT;


    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
        ui = FindObjectOfType<UIHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(ui.candy < 15 && ui.candy > 8 )
            {
                wScreen.sprite = winScreen1;
                wSO.SetActive(true);
                collision.GetComponent<SpriteRenderer>().enabled = false;
                wsT.text = "You escape the mansion with a bagful of candy in tow. But was that really all the treats you could've found?";
            }
            if(ui.candy >= 15)
            {
                wScreen.sprite = winScreen2;
                wSO.SetActive(true);
                collision.GetComponent<SpriteRenderer>().enabled = false;
                wsT.text = "Having cleaned out the entire manor, you sit proudly atop your heap of treats. You have now been declared the certified coolest kid in the neighbourhood.";
            }

        }
    }
}
