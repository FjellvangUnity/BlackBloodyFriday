using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WallTile : Tile
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
	[MenuItem("Assets/Create/Tiles/WallTile")]
	public static void CreateWaterTile()
	{
		string path = EditorUtility.SaveFilePanelInProject("Save Walltile", "New Walltile", "asset", "SaveWalltile", "Assets");
		if (path == null)
		{
			return;
		}
		AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<WallTile>(), path);
	}

#endif
}
