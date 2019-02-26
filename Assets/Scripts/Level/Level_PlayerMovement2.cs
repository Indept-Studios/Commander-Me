using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_PlayerMovement2 : MonoBehaviour
{
    public float maxSpeed = 3f;
    public float jumpSpeed = 3f;

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
