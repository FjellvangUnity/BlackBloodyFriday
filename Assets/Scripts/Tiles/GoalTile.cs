using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GoalTile : Tile
{
	public static int Count = 0;
	public static bool AWOKE = false;
	public static List<Vector3Int> tiles = new List<Vector3Int>();
	public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
	{
		tiles.Add(position);
		AWOKE = true;
		Debug.Log("TILE: " + Count);
		return base.StartUp(position, tilemap, go);
	}

	
#if UNITY_EDITOR
	[MenuItem("Assets/Create/Tiles/GoalTile")]
	public static void CreateWaterTile()
	{
		string path = EditorUtility.SaveFilePanelInProject("Save goaltile", "New goaltile", "asset", "Savegoaltile", "Assets");
		if (path == null)
		{
			return;
		}
		AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<GoalTile>(), path);
	}

#endif
}
