using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreesController : MonoBehaviour
{
    TreeInstance[] trees;

	void Start()
	{
		var terrain = GetComponent<Terrain>();
		trees = terrain.terrainData.treeInstances;

		foreach (var tree in trees)
		{
			Debug.Log(tree.position);
		}
	}
}
