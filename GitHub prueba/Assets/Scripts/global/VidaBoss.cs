using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaBoss : MonoBehaviour
{
    public static float vida = 1000;
    public static float vidaMax = 0;
    public int score = 5000;
    public bool invencible = false;

    private Animator anim;

    private void Start()
    {
        vidaMax = vida;
        anim = GetComponent<Animator>();
    }

   

    public void getDamage(float damage)
    {
        if (vida > 0 && !invencible)
        {
            vida -= damage;
            anim.Play("damage");
            StartCoroutine(invencibilidad());
            character.score += 30;
        }
        if (vida <= 0)
        {
            vida = 0;
            getKilled();
            character.score += score;
        }

    }

    void getKilled()
    {
        anim.Play("die");
        GameObject.Destroy(gameObject);
    }

    IEnumerator invencibilidad()
    {
        invencible = true;
        yield return new WaitForSeconds(0.7f);
        invencible = false;
    }

    
}
