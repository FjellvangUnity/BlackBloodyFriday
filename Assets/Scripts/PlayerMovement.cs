using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private float moveForce = 1f;
    [SerializeField]
    private float hitForce = 10f;
    private bool alive = true;
    Rigidbody2D rb;

    private Vector2 currentDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollisionEnter2D");
    }

    private void FixedUpdate()
    {
        if (alive)
        {
        
            if (Input.GetKey(KeyCode.UpArrow))
            {
                currentDirection = Vector2.up;
                rb.AddForce(Vector2.up * moveForce);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                currentDirection = Vector2.down;
                rb.AddForce(Vector2.down * moveForce);

            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                currentDirection = Vector2.right;
                rb.AddForce(Vector2.right * moveForce);

            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                currentDirection = Vector2.left;
                rb.AddForce(Vector2.left * moveForce);

            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(currentDirection * hitForce);
            }
        }

    }

}
