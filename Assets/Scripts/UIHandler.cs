using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIHandler : MonoBehaviour
{
    public TextMeshProUGUI candies;
    public TextMeshProUGUI keys;
    public int key;
    public int candy;

    public GameObject flashlightIMG;
    public GameObject ballIMG;

    private PlayerControl pC;
    
    // Start is called before the first frame update
    void Start()
    {
        pC = FindObjectOfType<PlayerControl>();
        UpdateBall();
        flashlightIMG.SetActive(false);
    }
    
    public void UpdateKey()
    {
        keys.text = "x" + key;
    }
    public void UpdateCandy()
    {
        candies.text = "x" + candy;
    }

    public void UpdateBall()
    {
        ballIMG.SetActive(pC.hasBall);

    }

    public void UpdateFlashlight()
    {
        flashlightIMG.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
