using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GoalTile : Tile
{
	public static int Count = 0;
	public static List<GoalTile> tiles = new List<GoalTile>();
	public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
	{
		Count++;
		tileNumber = Count + 0;
		tiles.Add(this);
		//Debug.Log("TILE: " + Count);
		return base.StartUp(position, tilemap, go);
	}
	public int tileNumber;

	
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
