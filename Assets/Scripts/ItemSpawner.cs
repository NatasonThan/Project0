using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public List<GameObject> item;
    public List<GameObject> food;
    public List<float> spawnWeights; 
    private float spawnRate = 5f;          
    private float minHeight = -3f;          
    private float maxHeight = 3f;

    private void OnEnable()
    {
        InvokeRepeating(nameof(ItemSpawn), spawnRate, spawnRate);
        InvokeRepeating(nameof(FoodSpawner), 3, 3);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(ItemSpawn));
        CancelInvoke(nameof(FoodSpawner));
    }

    private void ItemSpawn()
    {
        int index = GetWeightedRandomIndex(spawnWeights);
        GameObject prefab = item[index];

        GameObject instance = Instantiate(prefab, transform.position, Quaternion.identity);
        
        //สำหรับ item ที่อยากให้เกิดที่อื่น
        /*if (index == 4)
        {
            instance.transform.position += new Vector3(5, Random.Range(-5, -1), 0);
        }*/
        instance.transform.position += new Vector3(5, Random.Range(minHeight, maxHeight), 0);
    }

    private void FoodSpawner()
    {
        int index = Random.Range(0, food.Count);
        GameObject prefab = food[index];
        GameObject instance = Instantiate(prefab, transform.position, Quaternion.identity);
        instance.transform.position += new Vector3(5, Random.Range(minHeight, maxHeight), 0);
    }

    private int GetWeightedRandomIndex(List<float> weights)
    {
        float totalWeight = 0f;

        foreach (float weight in weights)
        {
            totalWeight += weight;
        }

        float randomValue = Random.Range(0, totalWeight);
        float cumulativeWeight = 0f;

        for (int i = 0; i < weights.Count; i++)
        {
            cumulativeWeight += weights[i];
            if (randomValue < cumulativeWeight)
            {
                return i;
            }
        }

        return 0;
    }
}
