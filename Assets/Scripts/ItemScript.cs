using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    private Collider2D coll;
    private UIHandler ui;
    private PlayerControl pC;
    public GameObject[] gItems = new GameObject[5];
    private GameObject sObject;
    private int i;
    private Animator anim;
    public AudioSource sfx;
    public AudioClip[] sfxs = new AudioClip[2];

    public enum ItemType { candy, fivexcandies, key, ball, flashlight, treasurechest }
    public ItemType item;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
        ui = FindObjectOfType<UIHandler>();
        sfx = GetComponent<AudioSource>();
        if (item == ItemType.treasurechest)
        {
            anim = GetComponent<Animator>();
            
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            
            pC = collision.GetComponent<PlayerControl>();
            if (item == ItemType.candy)
            {
                
                ++ui.candy;
                ui.UpdateCandy();
                StartCoroutine(DestroyObject());

            }
            if (item == ItemType.fivexcandies)
            {
                ui.candy += 5;
                ui.UpdateCandy();
                StartCoroutine(DestroyObject());

            }
            if (item == ItemType.key)
            {
                ++ui.key;
                ui.UpdateKey();
                StartCoroutine(DestroyObject());

            }
            if (item == ItemType.ball)
            {
                pC.hasBall = true;
                ui.UpdateBall();
                StartCoroutine(DestroyObject());
            }
            if (item == ItemType.flashlight)
            {
                ui.UpdateFlashlight();
                pC.BetterFlashlight();
                StartCoroutine(DestroyObject());
            }
            if (item == ItemType.treasurechest)
            {
                if (ui.key > 0)
                {
                    
                    --ui.key;
                    ui.UpdateKey();
                    ReRoll();
                    StartCoroutine(PlayChestAnim());
                    
                }
            }

        }
    }
    private IEnumerator DestroyObject()
    {
        yield return null;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        Destroy(coll);
        sfx.clip = sfxs[0];
        sfx.Play();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
    private IEnumerator PlayChestAnim()
    {
        yield return null;
        anim.SetBool("Opened", true);
        sfx.clip = sfxs[1];
        sfx.Play();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
    private void ReRoll()
    {
        if(pC.hasBall)
        {
            int i = Random.Range(0, 3);
            GameObject item = Instantiate(gItems[i]);
            item.transform.position = transform.position;

        }
        else if (pC.bFl)
        {
            int i = Random.Range(0, 4);
            GameObject item = Instantiate(gItems[i]);
            item.transform.position = transform.position;
        }
        else
        {
            int i = Random.Range(0, gItems.Length);
            GameObject item = Instantiate(gItems[i]);
            item.transform.position = transform.position;
        }
    }
}
