using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vida_damage : MonoBehaviour
{
    public float vida = 100;
    public float vidaMax;
    public bool invencible = false;
    public float tiempo_invencible;
    public int totalScore = 0;
    public SpriteRenderer spr;

    public GameObject canvas;

    public GameObject player;
 
    private Animator anim;

    private void Start()
    {
        vidaMax = vida;
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    public void restarVida(float cantidad)
    {
        if (!invencible && vida > 0)
        {
            anim.Play("Damage");
            vida -= cantidad;
            StartCoroutine(invencibilidad());
            if (totalScore >= 5)
            {
                totalScore -= 5;
            }
            if (totalScore < 5)
            {
                totalScore = 0;
            }
        }
        if (vida <= 0)
        {
            Time.timeScale = 0f;
            anim.updateMode = AnimatorUpdateMode.UnscaledTime;
            anim.Play("Death");
            

        }
    }

    public void sumarVida(float cantidad)
    {
        vida += cantidad;
        if(vida > vidaMax)
        {
            vida = vidaMax;
        }
    }

    IEnumerator invencibilidad()
    {
        invencible = true;
        yield return new WaitForSeconds(tiempo_invencible);
        invencible = false;
    }
    void GameOver()
    {
        
        canvas.GetComponent<PopupPause>().gameOver();
        anim.updateMode = AnimatorUpdateMode.Normal;
        gameObject.SetActive(false);
    }

    public float getVida()
    {
        return vida;
    }
}
