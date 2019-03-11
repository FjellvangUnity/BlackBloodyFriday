using Assets.Scripts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Pathfinding
{
	public class AStar : MonoBehaviour
	{
		public Tilemap Map;
		public int Width = 200;
		public int Height = 200;
		Stack<Vector3Int> Path = new Stack<Vector3Int>();

		private void Start()
		{
			Debug.Log(GoalTile.AWOKE);
		}
		private void Update()
		{
			Debug.Log(GoalTile.Count);
			if (GoalTile.Count > 0 && Path.Count <= 0)
			{
				ComputeGreedy(Map.WorldToCell(transform.position), Map.WorldToCell(GoalTile.tiles[0]));
			}
			else if (Path.Count > 0)
			{
				var current = Path.Pop();
				while (Path.Count > 0)
				{
					var next = Path.Pop();
					Debug.DrawLine(Map.GetCellCenterWorld(current), Map.GetCellCenterWorld(next), Color.blue, 1);
					current = next;
				}
			}
		}

		public IEnumerable<CoordWithWeight> Neightbors(Vector3Int current)
		{
			for (int i = -1; i <= 1; i++)
			{
				for (int j = -1; j <= 1; j++)
				{
					var point = new Vector3Int(current.x + i, current.y + j, 0);
					var tile = Map.GetTile(point);
					if (tile == null || tile != null && tile.GetType() != typeof(WallTile))
					{
						//TODO: ADD BOUNDS 
						if (i != 0 && j != 0)
						{
							yield return new CoordWithWeight(point, 2);
						}

						yield return new CoordWithWeight(point, 1);
					}
				}
			}
			//yield return new Vector3Int(current.x+1, current.y, 0);
			//yield return new Vector3Int(current.x, current.y+1, 0);
			//yield return new Vector3Int(current.x, current.y-1, 0);
			//yield return new Vector3Int(current.x-1, current.y, 0);
		}
		private double Heuistic(Vector3Int a, Vector3Int b)
		{
			return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
		}
		public class CoordWithWeight : IComparable
		{
			public Vector3Int Coord;
			public double Weight;
			public CoordWithWeight(Vector3Int coord, double weight)
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
				return Weight.CompareTo((obj as CoordWithWeight).Weight);
			}
		}
		public bool ComputeGreedy(Vector3Int start, Vector3Int goal)
		{
			var frontier = new PriorityQueue<CoordWithWeight>();
			var cameFrom = new Dictionary<Vector3Int, Vector3Int>();
			var costSoFar = new Dictionary<Vector3Int, double>();
			var goalFound = false;
			costSoFar[start] = 0;
			frontier.Enqueue(new CoordWithWeight(start, 0));
			while (frontier.Count > 0 && !goalFound)
			{
				var current = frontier.Dequeue();

				foreach (var point in Neightbors(current.Coord))
				{
					var newCost = costSoFar[current.Coord] + point.Weight;
					Debug.DrawLine(Map.GetCellCenterWorld(current.Coord), Map.GetCellCenterWorld(point.Coord), Color.red, 2);
					var tile = Map.GetTile(point.Coord);
					if (point.Coord == goal)
					{
						//goal = point;
						goalFound = true;
						cameFrom[point.Coord] = current.Coord;
						break;
					}

					if (!costSoFar.ContainsKey(point.Coord) || 
						newCost < costSoFar[point.Coord])
					{
						costSoFar[point.Coord] = newCost;

						var priority = newCost + Heuistic(goal, point.Coord);
						frontier.Enqueue(new CoordWithWeight(point.Coord, priority));

						cameFrom[point.Coord] = current.Coord;

					}
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
	}
}
