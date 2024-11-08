using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PowerUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            Pickup(other);
        }
    }
    
    void Pickup(Collider2D player)
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
        else if (gameObject.CompareTag("Bomb"))
        {
            gameManager.RemoveScore(5);
        }
        Destroy(gameObject);
    }

}
