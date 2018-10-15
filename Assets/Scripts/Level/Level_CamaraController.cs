using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_CamaraController : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - player.transform.position;
    }

    private void LateUpdate()
    {
            transform.position = player.transform.position + offset;
    }
}