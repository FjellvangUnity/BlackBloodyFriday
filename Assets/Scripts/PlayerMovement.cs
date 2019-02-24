using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private float moveForce = 10f;
    [SerializeField]
    private float hitForce = 50f;
    //[SerializeField]
    //private LayerMask layerMask;
    private bool alive = true;
    Rigidbody2D rb, enemyRb;
    AudioSource audioSource;

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
        audioSource = GetComponent<AudioSource>();
    }


    private void FixedUpdate()
    {
        if (alive)
        {
			var horizontal = Input.GetAxisRaw("Horizontal");
			var vertical = Input.GetAxisRaw("Vertical");
			animator.SetFloat("moving", rb.velocity.magnitude);
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


            if (Input.GetKeyDown(KeyCode.Space))
            {
                hit = Physics2D.Raycast(this.transform.position, relative, 1.5f, LayerMask.GetMask("Enemy"));


                if (hit.collider != null)
                {
                    rb.AddForce(-rb.velocity, ForceMode2D.Impulse);
                    enemyRb = hit.transform.GetComponent<Rigidbody2D>();
                    enemyRb.AddForce(currentDirection * hitForce, ForceMode2D.Impulse);
                    audioSource.Play();
                    hit = new RaycastHit2D();
                }
            }
        }


    }
}

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawLine(transform.position, currentDirection*5.0f);

    //}
