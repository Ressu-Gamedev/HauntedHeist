using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Rigidbody2D rb;
    // These are used by the animation system
    public float mHorizontal;
    public float mVertical;
    private Animator anim;
    private Vector2 lPos;
    public GameObject flashlight;
    public GameObject bFlashlight;
    public bool dead;

    public float oSpeed;
    private float speed;
    private bool lDirGot;

    //Ball Mechanic
    public bool hasBall;
    public bool bFl;
    public GameObject ball;
    private UIHandler ui;

    //Sounds
    private AudioSource sfx;
    public AudioClip[] sfxs = new AudioClip[2];
    private bool isWalking;

    //GameOver
    public GameObject gOS;

    // Start is called before the first frame update
    void Start()
    {
        speed = oSpeed;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        ui = FindObjectOfType<UIHandler>();
        sfx = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        mVertical = Input.GetAxis("Vertical");
        anim.SetFloat("mVert", mVertical);
        mHorizontal = Input.GetAxis("Horizontal");
        anim.SetFloat("mHori", mHorizontal);
        float mSpeed = Mathf.Abs(mHorizontal) + Mathf.Abs(mVertical);
        anim.SetFloat("mSpeed", mSpeed);

        if (mSpeed > 0.2f)
        {
            WalkingAudio();
            float angle = Mathf.Atan2(-mHorizontal, mVertical) * Mathf.Rad2Deg;
            flashlight.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            lDirGot = false;
            isWalking = true;
        }
        if(mSpeed < 0.2f && !lDirGot)
        { 
            float angle = Mathf.Atan2(-mHorizontal, mVertical) * Mathf.Rad2Deg;
            flashlight.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            anim.SetFloat("lVert", mVertical);
            anim.SetFloat("lHori", mHorizontal);
            isWalking = false;
            lDirGot = true;
        }
        if(Input.GetButton("Fire1"))
        {
            speed = oSpeed * 2f;
        }
        if(!Input.GetButton("Fire1"))
        {
            speed = oSpeed;
        }
        if(Input.GetButton("Fire2"))
        {
            UseBall();
        }
        WalkingAudio();
        if(dead)
        {
            StartCoroutine(GameOverStart());
        }
    }
    //
    void FixedUpdate()
    {
        Movement();

    }

    private void Movement()
    {
        if(!dead)
        {
            Vector2 movement = new Vector2(mHorizontal, mVertical);
            movement = Vector2.ClampMagnitude(movement, 1f);
            rb.velocity = (movement * speed);
        }
        else
        {
            Vector2 movement = new Vector2(0, 0);
            movement = Vector2.ClampMagnitude(movement, 1f);
            rb.velocity = (movement * speed);
        }
    }

    private void UseBall()
    {
        if(hasBall)
        {
            Instantiate(ball, transform.parent);
        }
        ui.UpdateBall();
    }
    public void BetterFlashlight()
    {
        sfx.clip = sfxs[1];
        sfx.loop = false;
        sfx.volume = .8f;
        sfx.Play();
        flashlight.SetActive(false);
        bFlashlight.SetActive(true);
        flashlight = bFlashlight;
        bFl = true;
    }

    private void WalkingAudio()
    {

        if (isWalking)
        {
            if(!sfx.isPlaying)
            {
                sfx.clip = sfxs[0];
                sfx.volume = 0.1f;
                sfx.Play();
                sfx.loop = true;
            }
            
        }
        else
        {   
            if(sfx.isPlaying)
            {
                if(sfx.clip == sfxs[0])
                {
                    sfx.Stop();
                }
                    
                
            }
        }

    }
    private IEnumerator GameOverStart()
    {
        yield return new WaitForSeconds(1f);
        gOS.SetActive(true);
    }
}
