using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_knockback : MonoBehaviour
{
    private Rigidbody2D rb;
    private Enemy_Movement enemy_movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy_movement = GetComponent<Enemy_Movement>();
    }
    public void Knockback(Transform playerTransfrom, float knockbackForce, float knockbackTime, float stunTime)
    {
        enemy_movement.ChangeState(EnemyState.Knockback);
        StartCoroutine(stunTimer(knockbackTime, stunTime));
        Vector2 direction = (transform.position - playerTransfrom.position).normalized;
        rb.velocity = knockbackForce * direction;


    }

    IEnumerator stunTimer(float knockbackTime, float stunTime)
    {
        yield return new WaitForSeconds(knockbackTime); //wait till knockback happens

        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(stunTime);
        enemy_movement.ChangeState(EnemyState.Idle);
    }
}
