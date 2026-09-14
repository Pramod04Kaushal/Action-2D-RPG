using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Combat : MonoBehaviour
{
    public int damage = 1;
    public Transform attackPoint;
    public float weaponRange;
    public float knockbackForce;
    public float stunTime;
    public LayerMask playerLayer;



    public void Attack()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, playerLayer);

        if(hitPlayer.Length > 0)
        {
            hitPlayer[0].GetComponent<PlayerHealth>().ChangeHealth(-damage);
            hitPlayer[0].GetComponent<PlayerMovement>().knockback(transform, knockbackForce, stunTime);
        }
    }
}
