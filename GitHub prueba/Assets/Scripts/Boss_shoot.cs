using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_shoot : MonoBehaviour
{

    //Script universal para mobs dentro del videojuego

    private Rigidbody2D rb; //variable RigidBody2D
    private Animator anim; //variable Animator
    private SpriteRenderer spr; //variable SpriteRenderer

    public int movHor = 0; //Indicador de la dirección horizontal del enemigo
    public float speed = 1f; //Velocidad del enemigo

    //Estados del enemigo
    public bool attack = false; //¿está atacando?
    public bool isMoving = false; //¿se está moviendo?
    public bool isGroundFloor = true; //¿está en contacto con el suelo
    public bool isGroundFront = false; //¿está en contacto con algún muro?

    public float distancia;
    public Vector2 player_pos; //Posición del jugador 
    public float distanciaMaximaEnemyPlayer = 2.5f; //(distancia máxima en la que el jugador es targeteado pro el enemigo)
    public float distancia_frenado = 1.5f; //(distancia de cercanía al jugador donde el enemigo se detiene para atacar)
    public float distancia_retraso = 2f; //(distancia de cercanía al jugador donde el enemigo retrocede)

    public float corregirDistanciaRayEnY = 0f; //Variable para corregir punto de origen del Raycast en el sprite
    public float distanciaColisionY = 0f; //Distancia definida desde el sistema para definir la distancia necesaria para detectar suelo
    public float distanciaColisionX = 0f; //Distancia definida desde el sistema para definir la distancia necesaria para detectar colisiones en el eje Y (horizontal)

    public bool isShooter;
    public bool targetPlayer = false;
    //Falta por implementar el ataque de los enemigos (hay que usar scripts aparte)

    public float velocida = 0;


    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();



    }

    // Update is called once per frame
    void Update()
    {
        if (VidaBoss.vida > VidaBoss.vidaMax)
        {
            VidaBoss.vida = VidaBoss.vidaMax;
        }

        //analisis de Raycast
        Vector3 auxX = new Vector3(transform.position.x, transform.position.y + corregirDistanciaRayEnY, transform.position.z);
        Debug.DrawRay(auxX, Vector3.down * distanciaColisionY, Color.red, 1);
        if (Physics2D.Raycast(auxX, Vector3.down, distanciaColisionY)) //si existe contacto con el suelo
        {
            if(Physics2D.Raycast(auxX, Vector3.down, distanciaColisionY).collider.CompareTag("suelo"))
                isGroundFloor = true;

        }
        else
        {
            isGroundFloor = false;
        }

        //nuevo vector 3 auxiliar para corregir altura del Raycast del enemigo 
        Vector3 aux = new Vector3(transform.position.x, transform.position.y + corregirDistanciaRayEnY, transform.position.z);

        //analisis de Raycast
        Debug.DrawRay(aux, new Vector3(movHor,0,0) * distanciaColisionX, Color.blue, 1);
        if (Physics2D.Raycast(aux, new Vector3(movHor, 0, 0), distanciaColisionX)) //si existe colision en eje X (horizontal)
        {
            if (Physics2D.Raycast(aux, new Vector3(movHor, 0, 0), distanciaColisionX).collider.gameObject.tag == "Enemy") //si existe colision en eje X con otro enemigo
            {
                movHor *= -1;
                flip(movHor);
            }
            else
            {
                // Colisión con jugador 
                if (!Physics2D.Raycast(aux, new Vector3(movHor, 0, 0), distanciaColisionX).collider.gameObject.CompareTag("Player") && !(Physics2D.Raycast(aux, new Vector3(movHor, 0, 0), distanciaColisionX).collider.gameObject.CompareTag("obs_enemy") && targetPlayer))
                {
                    isGroundFront = true;
                }
            }
        }
        else
        {
            isGroundFront = false;
        }

        if(findClosest()!=null)
            player_pos = new Vector2(findClosest().transform.position.x, findClosest().transform.position.y);

        distancia = Vector2.Distance(transform.position, player_pos);

        //Verificamos si el enemigo está a menos de la distancia máxima del jugador
        if (Vector2.Distance(transform.position, player_pos) <= distanciaMaximaEnemyPlayer)
        {
            velocida = speed * Time.deltaTime;
            targetPlayer = true;
            //1er caso: la distancia entre enemigo y jugador es mayor a la distancia de frenado 
            if (Vector2.Distance(transform.position, player_pos) > distancia_frenado)
            {
                if (!isGroundFloor || isGroundFront)
                {
                    isMoving = false;
                    transform.position = transform.position;
                }
                else
                {
                    //El enemigo no ataca y solo se limita a acercarse al jugador

                    attack = false;

                    isMoving = true;

                    //speed *= Time.deltaTime;

                    transform.position = Vector2.MoveTowards(transform.position, player_pos, speed * Time.deltaTime);

                    flip(player_pos.x - transform.position.x);
                }
            }
            if (isShooter)
            {
                //2do caso: la distancai entre enemigo y jugador es menor a la distancia de retroceso
                if (Vector2.Distance(transform.position, player_pos) <= distancia_retraso)
                {
                    //El enemigo no ataca y solo se limita a alejarse del jugador
                    isMoving = true;

                    transform.position = Vector2.MoveTowards(transform.position, player_pos, -speed * 2f * Time.deltaTime);

                    flip(player_pos.x - transform.position.x);
                }
                //3er caso: la distancia entre enemigo y jugador es menor o igual a la distancia de frenado y mayor a la distancia de retroceso
                if (Vector2.Distance(transform.position, player_pos) <= distancia_frenado && Vector2.Distance(transform.position, player_pos) > distancia_retraso)
                {
                    //El enemigo deja de moverse y comienza a atacar

                    //obtener un valor al azar (-1 ó 1) para definir la dirección de los enemigos al inicio
                    if (!attack)
                    {
                        int auxi = Random.Range(0, 4);
                        switch (auxi)
                        {
                            case 0:
                                anim.Play("shoot");
                                break;
                            case 1:
                                anim.Play("defense");
                                gameObject.GetComponent<VidaBoss>().invencible = true;
                                WaitForSeconds espera = new WaitForSeconds(3f);
                                gameObject.GetComponent<VidaBoss>().invencible = false;
                                break;
                            case 2:
                                anim.Play("laserShoot");
                                break;
                            case 3:
                                anim.Play("armor");
                                VidaBoss.vida += 100;
                                break;
                            case 4:
                                isShooter = false;
                                break;
                        }
                        StartCoroutine(ataque());
                    }
                    isMoving = false;
                    anim.SetBool("isMoving", isMoving);

                    transform.position = transform.position;

                    flip(player_pos.x - transform.position.x);
                }
            }
            else
            {
                if (Vector2.Distance(transform.position, player_pos) <= distancia_frenado)
                {
                    isMoving = false;
                    transform.position = transform.position;
                    if (!attack)
                    {
                        int auxi = Random.Range(0, 4);
                        switch (auxi)
                        {
                            case 0:
                                break;
                            case 1:
                                anim.Play("defense");
                                gameObject.GetComponent<VidaBoss>().invencible = true;
                                WaitForSeconds espera = new WaitForSeconds(3f);
                                gameObject.GetComponent<VidaBoss>().invencible = false;
                                break;
                            case 2:
                                break;
                            case 3:
                                anim.Play("armor");
                                VidaBoss.vida += 100;
                                break;
                            case 4:
                                anim.Play("melee");
                                break;
                        }
                        StartCoroutine(ataque());
                    }
                    flip(player_pos.x - transform.position.x);
                }
            }
        }
        //en el caso de haber una distancia mayor a la distancia máxima entre enemigo y jugador, el enemigo se comportará de manera normal sin atacar al no notar la presencia del jugador
        else
        {
            targetPlayer = false;

            isMoving = false;
            transform.position = transform.position;
        }
    }
    /*
    void FixedUpdate()
    {
        rb.velocity = new Vector2(movHor * speed, rb.velocity.y);

    }
    */
    //Encontrar al jugador más cercano
    public GameObject findClosest()
    {
        GameObject[] players;
        players = GameObject.FindGameObjectsWithTag("Player");
        GameObject closest = null;
        float distancia = Mathf.Infinity;
        Vector3 posicion = transform.position;
        foreach (GameObject player in players)
        {
            Vector3 diferenciaDistancia = player.transform.position - posicion;
            float currentDistancia = diferenciaDistancia.sqrMagnitude;
            if (currentDistancia < distancia)
            {
                closest = player;
                distancia = currentDistancia;
            }
        }
        return closest;
    }

    //Voltear sprite del enemigo según su dirección en el eje X (-1 o 1)
    private void flip(float valor)
    {
        if (valor < 0)
        {
            spr.flipX = true;
        }
        else
        {
            if (valor > 0)
            {
                spr.flipX = false;
            }
               
        }
    }

    IEnumerator ataque()
    {
        attack = true;
        yield return new WaitForSeconds(1.5f);
        attack = false;
    }
}

