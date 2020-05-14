using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public Transform player;
    public bool isFlipped = false;

    public Vector3 attackOffset;
    public int attackDamage = 20;
    public int enrageAttackDamage = 40;

    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask attackMask;

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -0.5f;

        if (transform.position.x-1f > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x+1f < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
    public void Attack()
    {

        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position,attackRange,attackMask);
        
        foreach(Collider2D player in hitPlayers)
        {
            GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBarScript>().takeDamage(attackDamage);
            break;
        }

    }

    public void EnragedAttack()
    {

        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, attackMask);

        foreach (Collider2D player in hitPlayers)
        {
            GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBarScript>().takeDamage(enrageAttackDamage);
            break;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Fire")
        {
            GameObject.FindGameObjectWithTag("BossBar").GetComponent<BossBarHealth>().takeDamage(30);
   
        }
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
