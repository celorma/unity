using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_shoot : MonoBehaviour
{
    private Rigidbody2D rb;

    private Animator anim;
    private SpriteRenderer spr;

    public float movHor = 0f;
    public float speed = 3f;

    public bool attack = false;
    public bool isMoving = false;
    public bool isGroundFloor = true;
    public bool isGroundFront = false;



    private Collider2D[] detectGroundResults;

    [SerializeField] private Collider2D detectTrigger;
    [SerializeField] private ContactFilter2D contactFilter;


    public LayerMask groundLayer;
    public float frontGrndRayDist = 0.25f;
    public float floorCheckY = 0.52f;
    public float frontCheck = 0.51f;
    public float frontDist = 0.001f;

    private Vector3 moveToPosition;

    public Transform player_pos;
    public float distancia_frenado = 3f;
    public float distancia_retraso = 2f;

    public Transform punto_salida;
    public GameObject fireball;
    private float tiempo;

    public int score = 50;

    private RaycastHit2D hit;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();

        player_pos = GameObject.Find("Character").transform;

    }

    // Update is called once per frame
    void Update()
    {

        if (movHor == 0)
        {

            if (Vector2.Distance(transform.position, player_pos.position) > distancia_frenado)
                transform.position = Vector2.MoveTowards(transform.position, player_pos.position, speed * Time.deltaTime);
            {
                isMoving = true;

                attack = true;


                anim.SetBool("attack", attack);

                anim.SetBool("isMoving", isMoving);

                flip(player_pos.position.x - transform.position.x);
            }

            if (Vector2.Distance(transform.position, player_pos.position) < distancia_retraso)
            {
                transform.position = Vector2.MoveTowards(transform.position, player_pos.position, -speed * Time.deltaTime);

                isMoving = true;

                attack = false;

                anim.SetBool("attack", attack);

                anim.SetBool("isMoving", isMoving);

                flip(player_pos.position.x - transform.position.x);
            }

            if (Vector2.Distance(transform.position, player_pos.position) < distancia_frenado && Vector2.Distance(transform.position, player_pos.position) > distancia_retraso)
            {
                transform.position = transform.position; 
                flip(player_pos.position.x - transform.position.x);

            }
            
           

        }
        /*
        else
        if (movHor != 0)
            isMoving = (movHor != 0);

            anim.SetBool("isMoving", isMoving);

            flip(movHor);
        //evitar caer en precipicio

            updateGround();

            updateWalls();

        /*
        isGroundFloor = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y - floorCheckY, transform.position.z), new Vector3(movHor,0,0), frontGrndRayDist, groundLayer);

        if (!isGroundFloor)
            movHor *= -1;
        */

        //choque con pared
        /*
        isGroundFront = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y - floorCheckY, transform.position.z), new Vector3(movHor, 0, 0), frontGrndRayDist, groundLayer);

        if (!isGroundFront)
            movHor *= -1;
        


            if (Physics2D.Raycast(transform.position, new Vector3(movHor, 0, 0), frontCheck, groundLayer))
                movHor *= -1;

            //choque con otro enemigo

            hit = Physics2D.Raycast(new Vector3(transform.position.x + movHor * frontCheck, transform.position.y, transform.position.z), new Vector3(movHor, 0, 0), frontDist);

            if (hit != null)
                if (hit.transform != null)
                    if (hit.transform.CompareTag("Enemy"))
                        movHor *= -1;
            */
        




    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(movHor * speed, rb.velocity.y);

    }

    private void updateGround()
    {
        isGroundFloor = detectTrigger.OverlapCollider(contactFilter, detectGroundResults) > 0;
        if (!isGroundFloor)
            movHor *= -1;
    }

    private void updateWalls()
    {
        isGroundFront = detectTrigger.OverlapCollider(contactFilter, detectGroundResults) > 0;
        if (!isGroundFront)
            movHor *= -1;
    }

    private void flip(float valor)
    {

        if (valor < 0)
            spr.flipX = true;
        else
        if (valor > 0)
            spr.flipX = false;

    }

    private void getKilled()
    {
        gameObject.SetActive(false);
    }
}

