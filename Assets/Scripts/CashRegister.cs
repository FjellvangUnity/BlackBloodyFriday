using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashRegister : MonoBehaviour
{
	AudioSource audioSource;
	Animator animator;
	private void Awake()
	{
		audioSource = GetComponentInParent<AudioSource>();
		animator = GetComponentInParent<Animator>();
	}
	// Start is called before the first frame update
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag.Equals("Player"))
		{
            ScoreManager.Instance.BuyItems();
			if (ScoreManager.Instance.totalItems > 0)
			{
				audioSource.Play();
				animator.SetTrigger("Trigger");

			}
            ScoreManager.Instance.totalItems = 0;
	    }
	}
}
