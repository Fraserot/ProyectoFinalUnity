using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed;
    public Rigidbody2D rb;
    private Animator animator;


    private int state;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
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
        rb.velocity = new Vector2(movement.x *speed, movement.y * speed);
    }
    //En un futuro pues, para organizar
    private void Animations()
    {
        


    }
}
