using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GoalTile : Tile
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
