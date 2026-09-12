using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 1.8f;
    public Rigidbody2D rb;
    public int facingDirection = 1; // 1 for right, -1 for left
    public Animator animator;
    


    // FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if ((horizontalInput > 0 && transform.localScale.x < 0) || (horizontalInput < 0 && transform.localScale.x > 0))
        {
            Flip();
        }

        animator.SetFloat("Horizontal", Mathf.Abs(horizontalInput)); // Mathf.Abs Turns all numbers into positive numbers
        animator.SetFloat("Vertical", Mathf.Abs(verticalInput));

        rb.velocity = new Vector2(horizontalInput, verticalInput) * speed;
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
