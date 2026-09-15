using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    public Rigidbody2D rb;
    public int facingDirection = 1; // 1 for right, -1 for left
    public Animator animator;
    
    private bool isKnockedBack;

    public Player_Combat playerCombat;


    private void Update()
    {
        if (Input.GetButtonDown("Slash"))
        {
            playerCombat.Attack();
        }
    }

    // FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        if (isKnockedBack == false)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            if ((horizontalInput > 0 && transform.localScale.x < 0) || (horizontalInput < 0 && transform.localScale.x > 0))
            {
                Flip();
            }

            animator.SetFloat("Horizontal", Mathf.Abs(horizontalInput)); // Mathf.Abs Turns all numbers into positive numbers
            animator.SetFloat("Vertical", Mathf.Abs(verticalInput));

            rb.velocity = new Vector2(horizontalInput, verticalInput) * StatsManager.Instance.speed; // Controls Player Velocity
        }


    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public void Knockback(Transform enemy, float force, float stunTime)
    {
        isKnockedBack = true;
        Vector2 direction = (transform.position - enemy.position).normalized;
        rb.velocity = direction * force;
        StartCoroutine(KnockbackCounter(stunTime));
    }

    IEnumerator KnockbackCounter(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        rb.velocity = Vector2.zero;
        isKnockedBack = false;
    }
}
