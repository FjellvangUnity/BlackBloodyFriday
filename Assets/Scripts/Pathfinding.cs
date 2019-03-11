using Assets.Scripts.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pathfinding : MonoBehaviour
{

	public Tilemap Map;
	public int Width = 200;
	public int Height = 200;
	public float timeToCompute = 5f;
	public float timeToFindNewPos = 5f;
	public float speed = 1;
	public float maxSpeed = 5;
	float timer = 0;
	float timerToFindNewPos = 0f;
	Stack<Vector3> Path = new Stack<Vector3>();
	int tileCounter = 0; // THIS IS A TEST variable and should be changed
	Rigidbody2D rig;

	Vector3 end = new Vector3();
	private bool pathfound;
	Vector3 nextPos;
	List<Vector3Int> goalTilesVisited = new List<Vector3Int>();
	Type[] tiles = new Type[] { typeof(GoalTile), typeof(CashPointTile) };

	// Start is called before the first frame update
	void Start()
    {
		var mapPos =  Map.WorldToCell(transform.position);
		var tile = Map.GetTile(mapPos);
		pathfound = ComputePath(mapPos, tiles[0]);
		nextPos = transform.position;
		rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
		var mapPos =  Map.WorldToCell(transform.position);
		var tile = Map.GetTile(mapPos);

		if (pathfound )//&& transform.position != end)
		{
			timerToFindNewPos += Time.deltaTime;
			if ((timerToFindNewPos > timeToFindNewPos || Vector3.Distance(transform.position, nextPos) <= 1) && Path.Count>0)
			{
				timerToFindNewPos = 0;
				nextPos = Path.Pop();
			}
			Vector3 relative = nextPos - transform.position;
			var angle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(Vector3.forward * angle);

			if (rig.velocity.magnitude < maxSpeed)
			{
				rig.AddForce(relative * speed * Time.deltaTime);
			}
			if (Path.Count == 0)
			{
				timer += Time.deltaTime;
				if (timer >= timeToCompute)
				{
					tileCounter++;
					timer = 0;

					pathfound = ComputePath(mapPos, tiles[UnityEngine.Random.Range(0,1)]);

				}
			}
		}
		else
		{
			pathfound = ComputePath(mapPos, tiles[0]);
		}
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
	private int Heuistic(Vector3Int a, Vector3Int b)
	{
		return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
	}
	class CoordWithWeight : IComparable
	{
		public Vector3Int Coord;
		int Weight;
		public CoordWithWeight(Vector3Int coord, int weight)
		{
			Coord = coord;
			Weight = weight;
		}
		public int CompareTo(CoordWithWeight other)
		{
			return Weight.CompareTo(other.Weight);	
		}

		public int CompareTo(object obj)
		{
			return Weight.CompareTo(obj);
		}
	}
	public bool ComputeGreedy(Vector3Int start, Vector3Int goal)
	{
		var frontier = new PriorityQueue<CoordWithWeight>();
		var visisted = new Dictionary<Vector3Int, bool>();
		var cameFrom = new Dictionary<Vector3Int, Vector3Int>();
		var goalFound = false;
		frontier.Enqueue(new CoordWithWeight(start, 0));
		while (frontier.Count > 0 && !goalFound)
		{
			var current = frontier.Dequeue();

			visisted[current.Coord] = true;

			foreach (var point in Neightbors(current.Coord))
			{
				Debug.DrawLine(current.Coord, point, Color.red);
				var priority = Heuistic(goal, point);
				var tile = Map.GetTile(point);
				if (point == goal)
				{
					//goal = point;
					goalFound = true;
					cameFrom[point] = current.Coord;
					break;
				}

				if (visisted.TryGetValue(point, out var x) || tile.GetType() == typeof(WallTile) || point.x > Width || point.y > Height)
				{
					continue;
				}

				frontier.Enqueue(new CoordWithWeight(point, priority));
				visisted[point] = true;

				cameFrom[point] = current.Coord;
			}
		}

		var next = goal;

		while (next != start)
		{
			Path.Push(next);
			next = cameFrom[next];
		}
		Path.Push(start);
		return true;
	}


	public bool ComputePath(Vector3Int startPosition, Type toFind)
	{
		if (!GoalTile.AWOKE)
		{
			return false;
		}
		var goalTilePosition = GoalTile.tiles[UnityEngine.Random.Range(0, GoalTile.Count)];
		var frontier = new Queue<Vector3Int>();
		frontier.Enqueue(startPosition);
		var visisted = new Dictionary<Vector3Int, bool>();
		var cameFrom = new Dictionary<Vector3Int, Vector3Int>();
		Vector3Int goal = new Vector3Int();
		var goalFound = false;

		while (frontier.Count > 0 && !goalFound)
		{
			var current = frontier.Dequeue();
			visisted[current] = true;

			foreach (var point in Neightbors(current))
			{

				var tile = Map.GetTile(point);
				if (tile != null && point == goalTilePosition)
				{
					goal = point;
					goalFound = true;
					cameFrom[point] = current;
					goalTilesVisited.Add(point);
					if (goalTilesVisited.Count == GoalTile.Count-1)
					{
						goalTilesVisited = new List<Vector3Int>();
					}
					break;
				}

				if (visisted.TryGetValue(point, out var x) || tile == null || tile.GetType() == typeof(WallTile) || point.x > Width || point.y > Height)
				{
					continue;
				}

				frontier.Enqueue(point);
				visisted[point] = true;

				cameFrom[point] = current;
			}
			//backtrack
		}

		var currentPos = goal;
		var y = Map.WorldToCell(transform.position);
		Path.Push(Map.GetCellCenterWorld(currentPos));
		while (currentPos != y)
		{
			if(!cameFrom.TryGetValue(currentPos, out var x))
			{
				Path = new Stack<Vector3>(); // empty it. dunno if needed
				return false;
			}
			Path.Push(Map.GetCellCenterWorld(x));

			currentPos = x;
		}
		end = Map.GetCellCenterWorld(goal);
		return true;	
	}

}
