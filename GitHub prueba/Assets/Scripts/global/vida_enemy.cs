using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vida_enemy : MonoBehaviour
{
    public float vida = 100;
    public int score = 50;
    public bool invencible = false;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void getKilled()
    {
        anim.Play("die");
        GameObject.Destroy(gameObject);
        character.score += score;
    }

    public void getDamage(float damage)
    {
        if (vida > 0 && !invencible)
        {
            vida -= damage;
            anim.Play("damage");
            StartCoroutine(invencibilidad());
            character.cargaSpec += 20;
            character.score += 10;
        }
        if (vida <= 0)
        {
            vida = 0;
            getKilled();

        }

    } 
    
    IEnumerator invencibilidad()
    {
        invencible = true;
        yield return new WaitForSeconds(0.7f);
        invencible = false;
    }
    
}
