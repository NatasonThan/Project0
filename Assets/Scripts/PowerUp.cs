using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    public float eat;
    public float reduceSpeed;
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
    
    void Pickup(Collider2D player) //Power Up ตรงนี้
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        Player stats = player.GetComponent<Player>();

        if (gameObject.CompareTag("Fish"))
        {
            gameManager.AddScore(2);
        }
        else if (gameObject.CompareTag("Seaweed"))
        {
            gameManager.AddScore(5);
        }
        else
        {

        }
        Destroy(gameObject);
    }

}
