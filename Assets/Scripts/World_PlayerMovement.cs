using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class World_PlayerMovement : MonoBehaviour
{
    public Vector2 movement;
    private new Rigidbody2D rigidbody;

    public Transform PlayerTransform { get { return transform; } }

    public float MovementSpeed { get { return 10; } }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Inputs();
        Move();
    }

    private void Inputs()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void SetMovement()
    {
        movement.Normalize();
        movement *= MovementSpeed;
    }

    public void Move()
    {
        Vector2 targetMovement = rigidbody.position + movement * Time.fixedDeltaTime;
        rigidbody.MovePosition(targetMovement);
    }

}
