using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pathfinding : MonoBehaviour
{

	public Tilemap Map;
	public int Width = 200;
	public int Height = 200;

	Vector3 end = new Vector3();
    // Start is called before the first frame update
    void Start()
    {
		var mapPos =  Map.WorldToCell(transform.position);
		var tile = Map.GetTile(mapPos);
		ComputePath(mapPos);
    }

    // Update is called once per frame
    void Update()
    {
		var mapPos =  Map.WorldToCell(transform.position);
		var tile = Map.GetTile(mapPos);
		//Debug.Log(string.Format("Tile: {0}, pos: {1}", tile, mapPos));
		Debug.DrawLine(transform.position, end);
    }



	public IEnumerable<Vector3Int> Neightbors(Vector3Int current)
	{
		for (int i = -1; i <= 1; i++)
		{
			for (int j = -1; j <= 1; j++)
			{
				yield return new Vector3Int(current.x + i, current.y+j, 0);
			}
		}
	}

	public bool ComputePath(Vector3Int startPosition)
	{
		var frontier = new Queue<Vector3Int>();
		frontier.Enqueue(startPosition);
		var visisted = new Dictionary<Vector3Int, bool>();
		var cameFrom = new Dictionary<Vector3Int, Vector3Int>();
		Vector3Int goal = new Vector3Int();
		var goalFound = false;

		Debug.Log("starting");
		while (frontier.Count > 0 && !goalFound)
		{
			var current = frontier.Dequeue();
			visisted[current] = true;

			foreach (var point in Neightbors(current))
			{
				Debug.DrawLine(transform.position, Map.GetCellCenterWorld(point), Color.red, 5);
				Debug.Log("checking: " + point);
				if (Map.GetTile(point) != null)
				{
					Debug.Log("FOUND TILE:" + point);
					Map.SetTile(point, null);
					goal = point;
					goalFound = true;
					break;
				}

				if (visisted.TryGetValue(point, out var x) || point.x > Width || point.y > Height)
				{
					// we've been here or we are out of bounds
					continue;
				}

				frontier.Enqueue(point);
				visisted[point] = true;

				cameFrom[point] = current;
			}
			//backtrack
			//var currentPos = goal;
			//while(currentPos != Map.WorldToCell(transform.position))
			//{
			//	Debug.DrawLine(Map.WorldToCell(cameFrom[currentPos]), Map.WorldToCell(currentPos));

			//	currentPos = cameFrom[currentPos];
			//}
		}

		end = Map.GetCellCenterWorld(goal);
		return false;	

	}

}
