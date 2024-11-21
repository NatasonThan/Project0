using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] prefab;
    private float spawnRate = 2f;
    private float minHeight = -1f;
    private float maxHeight = 1f;

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
        GameObject selectedPrefab;
        int index = Random.Range(0, 10);
        if (index < 9) 
        {
            selectedPrefab = prefab[Random.Range(0, prefab.Length)];
        }
        else
        {
            selectedPrefab = prefab[2];
        }

        GameObject pipes = Instantiate(selectedPrefab, transform.position, Quaternion.identity);

        if (selectedPrefab == prefab[2])
        {
            pipes.transform.position += Vector3.up * Random.Range(-5, -1);
            pipes.transform.position += Vector3.right * Random.Range(5, 10);
        }
        else
        {
            pipes.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
            pipes.transform.position += Vector3.right * Random.Range(5, 10);
        }
    }
}
