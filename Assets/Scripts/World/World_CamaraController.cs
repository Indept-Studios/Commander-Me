using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World_CamaraController : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset;
    private World_PlayerMovement playerMovement;

    public float camSens = 50;

    private void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<World_PlayerMovement>();

        offset = transform.position - player.transform.position;
    }

    private void LateUpdate()
    {
        if (playerMovement.movement != Vector2.zero)
        {
            transform.position = player.transform.position + offset;
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                MouseMovement();
            }
        }
    }

    private void MouseMovement()
    {
        transform.position += new Vector3(-Input.GetAxisRaw("Mouse X") * Time.deltaTime * camSens, -Input.GetAxisRaw("Mouse Y") * Time.deltaTime * camSens, 0);
    }

}