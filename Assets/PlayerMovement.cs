using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private float moveForce = 100f;
    [SerializeField]
    private float hitForce = 100f;
    private bool alive = true;
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        //_soundMaker = GetComponent<SoundMaker>();
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
                rb.AddForce(Vector2.up * hitForce);
            }
        }

    }

    void OnCollisionEnter()
    {
        //_soundMaker.PlayHitSound();
    }
}
