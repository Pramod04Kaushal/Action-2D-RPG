using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    public float speed;


    private int facingDirection = -1;
    private EnemyState enemyState, newState;

    private Rigidbody2D rb;
    private Transform player;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ChangeState(EnemyState.Idle);

    }



    // Update is called once per frame
    void Update()
    {
        if(enemyState == EnemyState.Chasing)
        {
            if (player.position.x > transform.position.x && facingDirection == -1 || 
                player.position.x < transform.position.x && facingDirection == 1)
            {
                Flip();
            }


            Vector2 direction = (player.position - transform.position).normalized; // Player position - Enemy position
            rb.velocity = direction * speed; 
        }

    }

    void Flip()
    {
        facingDirection *= -1; 
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (player == null)
            {
                player = collision.transform; 
            }
               

            ChangeState(EnemyState.Chasing);
        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rb.velocity = Vector2.zero;
            ChangeState(EnemyState.Idle);
        }
    }

    void ChangeState(EnemyState newState)
    {
        // Exit current state
        if (enemyState == EnemyState.Idle)
            anim.SetBool("isIdle", false);
        else if (enemyState == EnemyState.Chasing)
            anim.SetBool("isChasing", false);

        // Enter new state
        enemyState = newState;

        //Update the new Animation
        if (enemyState == EnemyState.Idle)
            anim.SetBool("isIdle", true);
        else if (enemyState == EnemyState.Chasing)
            anim.SetBool("isChasing", true);
    }



}

public enum EnemyState
{
    Idle,
    Chasing,

}


