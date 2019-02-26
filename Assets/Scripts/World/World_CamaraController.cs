using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World_CamaraController : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    private Vector3 offset;
    private World_PlayerMovement playerMovement;
   
    Vector3 playerTransformPosition;
    Vector3 CamaraTransformPosition;

    public float smoothSpeed = 0.025f;
    public float camSens = 50;
    public float freeWalk = 3;

    const float CAMARA_Z = -10f;

    private void Start()
    {
        playerMovement = player.GetComponent<World_PlayerMovement>();

        offset = CamaraTransformPosition - playerTransformPosition;
    }

    private void LateUpdate()
    {
        playerTransformPosition = new Vector3(player.transform.position.x, player.transform.position.y,CAMARA_Z);
        CamaraTransformPosition = new Vector3(transform.position.x, transform.position.y, CAMARA_Z);

        if (playerMovement.movement != Vector2.zero)
        {
            if (Vector3.Distance(playerTransformPosition, CamaraTransformPosition)>3)
            {
                Vector3 desiredPosition = playerTransformPosition + offset;
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
                transform.position = smoothedPosition;
            }
           
            //transform.position = playerTransformPosition + offset;
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
        transform.position += new Vector3(-Input.GetAxisRaw("Mouse X"), -Input.GetAxisRaw("Mouse Y"), 0) * Time.deltaTime * camSens;
    }

}