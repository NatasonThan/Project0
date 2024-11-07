using UnityEngine;

public class Velocity : MonoBehaviour
{
    public float speed = 5f;
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
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < leftEdge) 
        {
            Destroy(gameObject);
        }

        if (score.score >= 25) 
        {
            speed = 6f;
        }
    }

}
