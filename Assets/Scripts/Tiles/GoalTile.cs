using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GoalTile : Tile
{

	public bool pickedUP = false;
	public float timeToBeAlive = 10;
	float timer;
	// Start is called before the first frame update

	public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
	{
		pickedUP = false;
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
