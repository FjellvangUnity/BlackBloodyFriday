using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashRegister : MonoBehaviour
{
	// Start is called before the first frame update
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag.Equals("Player"))
		{
            ScoreManager.Instance.BuyItems();
            ScoreManager.Instance.totalItems = 0;
			Debug.Log("PLAYER TRIGGERED");
	    }
	}
}
