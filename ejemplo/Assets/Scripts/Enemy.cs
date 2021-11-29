using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;

    private Animator anim;
    private SpriteRenderer spr;

    public float movHor = 0f;
    public float speed = 3f;

    public bool isMoving = false;
    public bool isGroundFloor = true;
    public bool isGroundFront = false;

    public LayerMask groundLayer;
    public float frontGrndRayDist = 0.25f;
    public float floorCheckY = 0.52f;
    public float frontCheck = 0.51f;
    public float frontDist = 0.001f;

    public int score = 50;

    private RaycastHit2D hit;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //evitar caer en precipicio
        
        isGroundFloor = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y - floorCheckY, transform.position.z), new Vector3(movHor,0,0), frontGrndRayDist, groundLayer);

        if (!isGroundFloor)
            movHor *= -1;


        //choque con pared
        /*
        isGroundFront = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y - floorCheckY, transform.position.z), new Vector3(movHor, 0, 0), frontGrndRayDist, groundLayer);

        if (!isGroundFront)
            movHor *= -1;
        */

        if (Physics2D.Raycast(transform.position, new Vector3(movHor, 0, 0), frontCheck, groundLayer))
            movHor *= -1;

        //choque con otro enemigo
        
        hit = Physics2D.Raycast(new Vector3(transform.position.x + movHor * frontCheck,  transform.position.y, transform.position.z), new Vector3(movHor, 0, 0));

         if (hit != false)
             if (hit.collider != null)
                 if (hit.collider.CompareTag("Enemy"))
                     movHor *= -1;
         
        flip(movHor);

        if (movHor != 0)
            isMoving = true;

        anim.SetBool("isMoving", isMoving);
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(movHor * speed, rb.velocity.y);

    }

    private void flip(float valor)
    {
        Vector3 theScale = transform.localScale;

        if (valor < 0)
            theScale.x = Mathf.Abs(theScale.x) * -1;
        else
        if (valor > 0)
            theScale.x = Mathf.Abs(theScale.x);

        transform.localScale = theScale;
    }

    private void getKilled()
    {
        gameObject.SetActive(false);
    }
}
