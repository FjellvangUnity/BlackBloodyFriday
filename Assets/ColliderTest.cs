using Assets.Scripts.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTest : MonoBehaviour
{
	// Start is called before the first frame update
	public GameObject Player;
	public bool ShowLines;
	public float viewDist = 5;
	public float epsilon = 0.1f;
	BoxCollider2D col;
    void Start()
    {
		col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		if (ShowLines)
		{
			var cen = col.bounds.center;
			var topRight = col.bounds.max + new Vector3(epsilon, epsilon);
			var botRight = col.bounds.center + new Vector3(col.bounds.extents.x + epsilon, -col.bounds.extents.y - epsilon);
			var botLeft = col.bounds.min + new Vector3(-epsilon, -epsilon);
			var topLeft = col.bounds.center + new Vector3(-col.bounds.extents.x - epsilon, col.bounds.extents.y + epsilon);
			var play = Player.transform.position;
			Debug.DrawLine(cen, play);
			Debug.DrawLine(topRight, play, Color.red);
			Debug.DrawLine(botLeft, play, Color.cyan);
			Debug.DrawLine(topLeft, play, Color.green);
			Debug.DrawLine(botRight, play, Color.blue); 
		}
		VisibilityAlgoritm();
    }

	void VisibilityAlgoritm()
	{
		//var extents = col.bounds.extents;
		var topRight = col.bounds.max + new Vector3(epsilon, epsilon);
		var botRight = col.bounds.center + new Vector3(col.bounds.extents.x + epsilon, -col.bounds.extents.y - epsilon);
		var botLeft = col.bounds.min + new Vector3(-epsilon, -epsilon);
		var topLeft = col.bounds.center + new Vector3(-col.bounds.extents.x - epsilon, col.bounds.extents.y + epsilon);
		var playerPos = Player.transform.position;

		CastLine(topRight, playerPos);
		CastLine(botRight, playerPos);
		CastLine(topLeft, playerPos);
		CastLine(botLeft, playerPos);
	}
	
	void CastLine(Vector2 from, Vector3 to)
	{
		var hit = Physics2D.Raycast(from, to);
		if (hit.collider != null && hit.collider.CompareTag("Player"))
		{
			Debug.Log(hit.collider.name);
			Debug.DrawLine(from, Player.transform.position, Color.magenta);
		}
		else if (hit.collider != null)
		{
			Debug.Log(hit.collider.name);
		}
	}

}
