using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using JetBrains.Annotations;

public class PlayerMovement : NetworkBehaviour
{

    [SerializeField] private float speed;
    public Rigidbody2D rb;
    private Animator animator;
    public GameObject cameraHolder;
    private int state;

    private void Start()
    {
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        if (!IsOwner) return;

        Vector2 movement = Vector2.zero;

        if (Input.GetKey(KeyCode.A))
        {
            movement += Vector2.left;
            state = 2;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement += Vector2.right;
            state = 3;
        }
        if (Input.GetKey(KeyCode.W))
        {
            movement += Vector2.up;
            state = 0;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement += Vector2.down;
            state = 1;
        }
        animator.SetInteger("State", state);
        movement = movement.normalized;
        rb.velocity = new Vector2(movement.x * speed, movement.y * speed);
    }
    //Confirmacion de camara Online
    public override void OnNetworkSpawn()
    {
        cameraHolder.SetActive(IsOwner);
        base.OnNetworkSpawn();
    }



}

