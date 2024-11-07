using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawn : MonoBehaviour
{
    public List<GameObject> prefabs;
    public float spawnRate;
    public float minHeight;
    public float maxHeight;

    private void OnEnable()
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }

    private void Spawn()
    {
        int index = Random.Range(0, prefabs.Count);
        GameObject prefab = prefabs[index];
        GameObject instance = Instantiate(prefab, transform.position, Quaternion.identity);
        instance.transform.position += new Vector3(5,Random.Range(minHeight,maxHeight),0);
    }
}
