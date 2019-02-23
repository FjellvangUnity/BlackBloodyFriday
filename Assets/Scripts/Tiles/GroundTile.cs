using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundTile : Tile
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
	[MenuItem("Assets/Create/Tiles/GroundTile")]
	public static void CreateWaterTile()
	{
		
		string path = EditorUtility.SaveFilePanelInProject("Save Groundtile", "New Groundtile", "asset", "SaveGroundtile", "Assets");
		if (path == null)
		{
			return;
		}
		AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<GroundTile>(), path);
	}

#endif
}
