using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    public float eat = 0.1f;
    public float reduceSpeed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            Pickup(other);
        }
    }
    void Pickup(Collider2D player) 
    {
        Player stats = player.GetComponent<Player>();
        player.transform.localScale *= 1.1f;
        stats.strength -= eat;
        Destroy(gameObject);
    }
}
