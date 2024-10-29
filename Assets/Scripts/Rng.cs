using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rng : MonoBehaviour
{
    [SerializeField] GameObject[] itemPrefab;
    [SerializeField] float timeSpawn = 10f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ItemSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ItemSpawn() 
    {
        while (true) 
        {
            yield return new WaitForSeconds(timeSpawn);
            var wanted = Random.Range(-5, 5);
            var position = new Vector3(wanted, wanted);
            GameObject gameObject = Instantiate(itemPrefab[Random.Range(0,itemPrefab.Length)],
                position,Quaternion.identity);
            yield return new WaitForSeconds(timeSpawn);
            Destroy(gameObject, 5);
        }
    }

}
