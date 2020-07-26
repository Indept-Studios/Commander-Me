using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World_PlayerMovement : MonoBehaviour
{
    public float maxSpeed = 3;

    public Vector2 movement;

    protected Rigidbody2D rb2d;
   
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Inputs();
    }

    void FixedUpdate()
    {
        Movement();
    }


    void Inputs()
    {
        movement = Vector2.zero;

        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
    }

    public void Movement()
    {
        rb2d.position += (movement * Time.fixedDeltaTime) * maxSpeed;
    }

}