using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_shoot : MonoBehaviour
{

    //Script universal para mobs dentro del videojuego

    private Rigidbody2D rb; //variable RigidBody2D
    private Animator anim; //variable Animator
    private SpriteRenderer spr; //variable SpriteRenderer

    public int movHor = 0; //Indicador de la dirección horizontal del enemigo
    public float speed = 3f; //Velocidad del enemigo

    //Estados del enemigo
    public bool attack = false; //¿está atacando?
    public bool isMoving = false; //¿se está moviendo?
    public bool isGroundFloor = true; //¿está en contacto con el suelo
    public bool isGroundFront = false; //¿está en contacto con algún muro?

    public float distancia;
    public Transform player_pos; //Posición del jugador 
    public float distanciaMaximaEnemyPlayer = 2.5f; //(distancia máxima en la que el jugador es targeteado pro el enemigo)
    public float distancia_frenado = 1.5f; //(distancia de cercanía al jugador donde el enemigo se detiene para atacar)
    public float distancia_retraso = 2f; //(distancia de cercanía al jugador donde el enemigo retrocede)

    public float corregirDistanciaRayEnY = 0f; //Variable para corregir punto de origen del Raycast en el sprite
    public float distanciaColisionY = 0f; //Distancia definida desde el sistema para definir la distancia necesaria para detectar suelo
    public float distanciaColisionX = 0f; //Distancia definida desde el sistema para definir la distancia necesaria para detectar colisiones en el eje Y (horizontal)

    //Falta por implementar el ataque de los enemigos (hay que usar scripts aparte)

    public Transform punto_salida;
    public GameObject fireball;
    private float tiempo = 0;

    private RaycastHit2D hit;

    
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
        //obtener un valor al azar (-1 ó 1) para definir la dirección de los enemigos al inicio
        if (movHor == 0)
        {
            int auxi = Random.Range(-1, 1);
            if (auxi != 0)
            {
                movHor = auxi;
            }
            transform.Translate(speed*movHor * Time.deltaTime, 0, 0);
            flip(movHor);
        }
        

        //analisis de Raycast
        Debug.DrawRay(transform.position, Vector3.down * distanciaColisionY, Color.red, 1);
        if (Physics2D.Raycast(transform.position, Vector3.down, distanciaColisionY)) //si existe contacto con el suelo
        {
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
                if (Physics2D.Raycast(aux, new Vector3(movHor,0,0), distanciaColisionX).collider.gameObject.tag != "Player")
                {
                    isGroundFront = true;
                }
            }
        }
        else
        {
            isGroundFront = false;
        }


        player_pos = findClosest().transform;

        distancia = Vector2.Distance(transform.position, player_pos.position);

        //Verificamos si el enemigo está a menos de la distancia máxima del jugador
        if (Vector2.Distance(transform.position, player_pos.position) <= distanciaMaximaEnemyPlayer)
        {
            //1er caso: la distancia entre enemigo y jugador es mayor a la distancia de frenado 
            if (Vector2.Distance(transform.position, player_pos.position) > distancia_frenado)
            {
                if (!isGroundFloor || isGroundFront)
                {
                    anim.SetBool("isMoving", false);
                    transform.position = transform.position;
                }

                //El enemigo no ataca y solo se limita a acercarse al jugador

                attack = false;
                anim.SetBool("attack", attack);

                isMoving = true;
                anim.SetBool("isMoving", isMoving);

                transform.position = Vector2.MoveTowards(transform.position, player_pos.position, speed * Time.deltaTime);
                
                flip(player_pos.position.x - transform.position.x);
            }
            //2do caso: la distancai entre enemigo y jugador es menor a la distancia de retroceso
            if (Vector2.Distance(transform.position, player_pos.position) <= distancia_retraso)
            {
                if (!isGroundFloor || isGroundFront)
                {
                    isMoving = false;
                    anim.SetBool("isMoving", isMoving);
                    transform.position = transform.position;
                }
                else
                {
                    isMoving = true;
                    anim.SetBool("isMoving", isMoving);

                    transform.position = Vector2.MoveTowards(transform.position, player_pos.position, -speed * 2f * Time.deltaTime);

                    flip(player_pos.position.x - transform.position.x);
                }

                //El enemigo no ataca y solo se limita a alejarse del jugador

                
            }
            //3er caso: la distancia entre enemigo y jugador es menor o igual a la distancia de frenado y mayor a la distancia de retroceso
            if (Vector2.Distance(transform.position, player_pos.position) <= distancia_frenado && Vector2.Distance(transform.position, player_pos.position) > distancia_retraso)
            {
                //El enemigo deja de moverse y comienza a atacar

                attack = true;
                anim.SetBool("attack", attack);

                isMoving = false;
                anim.SetBool("isMoving", isMoving);

                transform.position = transform.position;

                flip(player_pos.position.x - transform.position.x);
            }
            
        }
        //en el caso de haber una distancia mayor a la distancia máxima entre enemigo y jugador, el enemigo se comportará de manera normal sin atacar al no notar la presencia del jugador
        else
        {
            if (!isGroundFloor) //Si el enemigo pierde contacto con suelo, dará la vuelta
            {
                movHor *= -1;
                flip(movHor);
            }

            if (isGroundFront) //Si el enemigo entra en contacto con un muro, se dará la vuelta
            {
                movHor *= -1;
                flip(movHor);
            }
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(movHor * speed, rb.velocity.y);

    }

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
    
}

