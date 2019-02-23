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
    Rigidbody2D rb, enemyRb;

    private Vector3 currentDirection;
    RaycastHit2D hit;

    private void Start()
    {
        currentDirection = new Vector2(1, 0);
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        currentDirection = transform.InverseTransformDirection(rb.velocity);
        //Debug.DrawLine(transform.position, transform.position + currentDirection.normalized * 5.0f, Color.red);
    }

    private void FixedUpdate()
    {
        if (alive)
        {
        
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb.AddForce(Vector2.up * moveForce);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
              
                rb.AddForce(Vector2.down * moveForce);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
              
                rb.AddForce(Vector2.right * moveForce);

            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
             
                rb.AddForce(Vector2.left * moveForce);

            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                hit = Physics2D.Raycast(transform.position, currentDirection);
                if (hit.collider != null)
                {
                    enemyRb = hit.transform.GetComponent<Rigidbody2D>();
                    enemyRb.AddForce(currentDirection * hitForce);
                    Debug.Log(enemyRb.name);
                }
            }
        }

    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawLine(transform.position, currentDirection*5.0f);
        
    //}

}
