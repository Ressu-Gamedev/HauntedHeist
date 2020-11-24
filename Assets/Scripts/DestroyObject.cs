using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    private Collider2D coll;
    private EnemyBehaviour eB;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
        eB = GetComponentInParent<EnemyBehaviour>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<PlayerControl>().dead = true;
            col.GetComponent<Animator>().SetBool("Dead", true);
        }
    }

}
