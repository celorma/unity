using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDamage : MonoBehaviour
{
    public GameObject explotion;
    public float damageBala = 30;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<vida_enemy>().getDamage(damageBala);
        }
        if (!collision.CompareTag("Player"))
        {
            GameObject clon = Instantiate(explotion, transform.position, transform.rotation) as GameObject;
            Destroy(clon, 0.5f);
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(ContactPoint2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>() , collision.collider);
        }
    }
}
