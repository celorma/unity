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
        if (collision.CompareTag("boss"))
        {
            collision.GetComponent<VidaBoss>().getDamage(damageBala);
        }
        if (!collision.CompareTag("Player") && !collision.CompareTag("Untagged") && !collision.CompareTag("obs_enemy"))
        {
            GameObject clon = Instantiate(explotion, transform.position, transform.rotation) as GameObject;
            Destroy(clon, 0.5f);
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("obs_enemy"))
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
        }
    }
}
