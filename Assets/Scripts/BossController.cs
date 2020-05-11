using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public Transform player;
    public bool isFlipped = false;

    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask attackMask;
    public int attackDamage = 20;
    public int enrageAttackDamage = 40;
    private bool triggered = false;
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
        /*        Debug.Log("attack1");
                Vector3 pos = transform.position;
                pos += transform.right * attackOffset.x;
                pos += transform.up * attackOffset.y;

                Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
                if (colInfo != null)
                {
                    Debug.Log("TATASRASRSAASRARSASRA");*/
        // }


        //TODO ATTACK BY BOSS
        if (triggered)
        {
            GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBarScript>().takeDamage(attackDamage);
            triggered = false;
        }
    }
    public void EnragedAttack()
    {
        //TODO ATTACK BY RAGED BOSS
        Debug.Log("enragedAttack");
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            Debug.Log("BUUUM");
            colInfo.GetComponent<HealthBarScript>().takeDamage(attackDamage);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Fire")
        {
            GameObject.FindGameObjectWithTag("BossBar").GetComponent<BossBarHealth>().takeDamage(30);
   
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            triggered = true;
        }
    }
}
