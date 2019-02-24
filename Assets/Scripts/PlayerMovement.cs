﻿using System;
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
    //[SerializeField]
    //private LayerMask layerMask;
    private bool alive = true;
    Rigidbody2D rb, enemyRb;

    private Vector3 currentDirection;
    RaycastHit2D hit;
	Animator animator;
	Vector3 relative = Vector3.right;

    private void Start()
    {
        currentDirection = new Vector2(1, 0);
		animator = GetComponentInChildren<Animator>();
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        if (alive)
        {
			var horizontal = Input.GetAxisRaw("Horizontal");
			var vertical = Input.GetAxisRaw("Vertical");
			animator.SetFloat("moving", rb.velocity.magnitude);
			Debug.Log(string.Format("hor {0}, vert{1}", horizontal, vertical));
            if (vertical > 0)
            {
                rb.AddForce(Vector2.up * moveForce);
				relative = currentDirection;
            }
            if (vertical < 0)
            {
                rb.AddForce(Vector2.down * moveForce);
				relative = currentDirection;
            }
            if (horizontal > 0)
            {
                rb.AddForce(Vector2.right * moveForce);
                relative = currentDirection;
            }
            if (horizontal < 0)
            {
                rb.AddForce(Vector2.left * moveForce);
				relative = currentDirection;
            }

            currentDirection = rb.velocity.normalized;
            //transform.rotation = Quaternion.Euler(currentDirection.x, 0, currentDirection.y);
            var angle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);

            Debug.Log(rb.velocity.normalized);

            Debug.DrawRay(this.transform.position, currentDirection, Color.red, 2.0f);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                hit = Physics2D.Raycast(this.transform.position, currentDirection, 1.5f, LayerMask.GetMask("Enemy"));


                if (hit.collider != null)
                {
                    Debug.Log(hit.transform.name);
                    rb.AddForce(-rb.velocity, ForceMode2D.Impulse);
                    enemyRb = hit.transform.GetComponent<Rigidbody2D>();
                    enemyRb.AddForce(currentDirection * hitForce, ForceMode2D.Impulse);
                    hit = new RaycastHit2D();
                    //Debug.Log(enemyRb.name);
                }
            }
        }


    }
}

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawLine(transform.position, currentDirection*5.0f);

    //}
