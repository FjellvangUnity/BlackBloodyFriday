using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashRegister : MonoBehaviour
{
	AudioSource audioSource;
	private void Awake()
	{
		audioSource = GetComponentInParent<AudioSource>();
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

			}
            ScoreManager.Instance.totalItems = 0;
	    }
	}
}
