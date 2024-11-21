using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Velocity : MonoBehaviour
{
    public float speed = 5f;
    public float targetSpeed;
    private float leftEdge;
    private GameManager score;

    private void Awake()
    {
        score = FindObjectOfType<GameManager>();
    }
    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
    }

    private void Update()
    {
        AdjustSpeedByScore();
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < leftEdge)
        {
            StartCoroutine(DestroyAfterDelay());
        }
    }
    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);                 
    }

    private void AdjustSpeedByScore()
    {
        if (score.score >= 100)
        {
            speed = 7f + targetSpeed;
        }
        else if (score.score >= 50)
        {
            speed = 6.5f + targetSpeed;
        }
        else if (score.score >= 25)
        {
            speed = 6f + targetSpeed;
        }
    }
}
