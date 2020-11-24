using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public float mHorizontal;
    public float mVertical;
    private Rigidbody2D rb;
    public float speed;
    public Transform player;
    public List<Transform> cPoints;
    public float startCPTime;
    private float cpTime;
    private int i;
    public float sightDistance;
    private bool pSeen;
    private Vector3 lPos;
    public bool notGhost;
    private Collider2D col;
    private bool cPlayer;
    private float cT;

    //Animation stuff
    private Animator anim;
    private bool lDirGot;

    //Sounds
    private AudioSource sfx;
    public AudioClip[] sfxs = new AudioClip[2];
    private bool isWalking;

    void Start()
    {
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        i = Random.Range(0, cPoints.Count);
        sfx = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        PlanMovement();

    }
    void FixedUpdate()
    {
        Movement();
        //DetectPlayer();

    }
    //void for when detecting a player (might use a raycast)
    private void DetectPlayer()
    {
        if(notGhost)
        {/*
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(mHorizontal, mVertical), sightDistance);
            Debug.DrawRay(transform.position, new Vector2(mHorizontal, mVertical), Color.green);
            //if (Physics2D.Raycast(transform.position, new Vector2(mHorizontal, mVertical), sightDistance))
            {
            if (hit)
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    Debug.Log("I see player");
                    player = hit.collider.transform;
                    pSeen = true;
                }
                if (!hit.collider.gameObject.CompareTag("Player") && pSeen)
                {
                    //if (player != null)
                    lPos = new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y);
                    cT = 4f;
                    cPlayer = true;
                    pSeen = false;
                }
            }
            */
        }
       

    }

    private void PlanMovement()
    {
        if(cPlayer)
        {
            mHorizontal = Mathf.Clamp(lPos.x - transform.position.x, -1f, 1f);
            mVertical = Mathf.Clamp(lPos.y - transform.position.y, -1f, 1f);
            if (cT < 0f)
            {
                cPlayer = false;
                player = null;
                
            }
            cT -= Time.deltaTime;
        }
        else
        {
            if (player == null)
            {

                if (cpTime < 0f)
                {
                    cpTime = startCPTime;
                    i = Random.Range(0, cPoints.Count);
                }
                mHorizontal = Mathf.Clamp(cPoints[i].transform.position.x - transform.position.x, -1f, 1f);
                mVertical = Mathf.Clamp(cPoints[i].transform.position.y - transform.position.y, -1f, 1f);

            }
            else
            {
                mHorizontal = Mathf.Clamp(player.transform.position.x - transform.position.x, -1f, 1f);
                mVertical = Mathf.Clamp(player.transform.position.y - transform.position.y, -1f, 1f);
            }

            anim.SetFloat("mVert", mVertical);
            anim.SetFloat("mHori", mHorizontal);

            //anim handling

            float mSpeed = Mathf.Abs(mHorizontal) + Mathf.Abs(mVertical)*2;
            anim.SetFloat("mSpeed", mSpeed);

            if (mSpeed > 0.2f)
            {
                WalkingAudio();
                lDirGot = false;
                isWalking = true;
                //float angle = Mathf.Atan2(mHorizontal, -mVertical) * Mathf.Rad2Deg;
                //sight.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            if (mSpeed < 0.2f && !lDirGot)
            {
                //float angle = Mathf.Atan2(mHorizontal, -mVertical) * Mathf.Rad2Deg;
                //sight.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                if (notGhost)
                { 
                    anim.SetFloat("lVert", mVertical);
                    anim.SetFloat("lHori", mHorizontal);
                }
                isWalking = false;
                lDirGot = true;


            }

            cpTime -= Time.deltaTime;
        }
        
    }

    private void Movement()
    {
        Vector2 movement = new Vector2(mHorizontal, mVertical);
        movement = Vector2.ClampMagnitude(movement, 1f);
        rb.velocity = (movement * speed);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.transform;
            cPlayer = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            lPos = new Vector3(collision.transform.position.x, collision.transform.position.y, 0);
            cPlayer = true;
            cT = 4f;
        }
    }
    private void WalkingAudio()
    {
        if(notGhost)
        {
            if (isWalking)
            {
                if (!sfx.isPlaying)
                {
                    sfx.clip = sfxs[0];
                    sfx.volume = 1f;

                    sfx.Play();
                    sfx.loop = true;
                }

            }
            else
            {
                if (sfx.isPlaying)
                {
                    if (sfx.clip == sfxs[0])
                    {
                        sfx.Stop();
                    }


                }
            }

        }
        else
        {
            if (isWalking)
            {
                if (!sfx.isPlaying)
                {
                    sfx.clip = sfxs[1];
                    sfx.volume = 0.1f;
                    sfx.Play();
                    sfx.loop = true;
                }

            }
            else
            {
                if (sfx.isPlaying)
                {
                    if (sfx.clip == sfxs[1])
                    {
                        sfx.Stop();
                    }


                }
            }

        }

    }
}
