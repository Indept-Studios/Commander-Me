using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_PlayerMovement2 : MonoBehaviour
{
    [SerializeField] float walkSpeed = 3f;
    [SerializeField] float movementSpeed = 3f;
    [SerializeField] float jumpSpeed = 1.25f;
    [SerializeField] float bonusSpeed = 0;

    public Vector2 movementIntense;

    public Vector2 velocity;
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
        movementIntense = Vector2.zero;
        movementIntense.x = Input.GetAxis("Horizontal") * walkSpeed;
        movementIntense.y = Input.GetAxis("Vertical") * jumpSpeed;
    }

    public void Movement()
    {
        rb2d.position += (movementIntense * Time.fixedDeltaTime) * (movementSpeed + bonusSpeed);
    }
}
