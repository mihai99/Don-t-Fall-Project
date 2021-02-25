using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TreeDestroyer : MonoBehaviour
{
    private Terrain Terrain;
    public Transform Parent;
    public List<GameObject> TreeObjects;

    private List<GameObject> TreesGameObjects;
    private TreeInstance[] oldTrees;
    private void Start()
    {
        Terrain = this.GetComponent<Terrain>();
        TreesGameObjects = new List<GameObject>();
        oldTrees = Terrain.terrainData.treeInstances;
        for (int i = 0; i < oldTrees.Length; i++)
        {
            var treeNumber = (int)Random.Range(0, TreeObjects.Count);
            var newTree = Instantiate(TreeObjects[treeNumber], ConvertTreePosition(oldTrees[i]), Quaternion.identity);
            newTree.transform.parent = Parent;
            TreesGameObjects.Add(newTree);
        }
        Terrain.terrainData.treeInstances = new TreeInstance[0];
        SetTrees();
    }

    private Vector3 ConvertTreePosition(TreeInstance tree)
    {
        float width = Terrain.terrainData.size.x;
        float height = Terrain.terrainData.size.z;
        float y = Terrain.terrainData.size.y;
      
        return new Vector3(tree.position.x * width, tree.position.y * y, tree.position.z * height) + Terrain.transform.position;
    }

    private void OnApplicationQuit()
    {
        this.Terrain.terrainData.treeInstances = oldTrees;
    }

    public void SetTrees()
    {
        foreach (var tree in TreesGameObjects)
        {
            tree.GetComponent<HideTree>().SetTree();
        }
    }
}
