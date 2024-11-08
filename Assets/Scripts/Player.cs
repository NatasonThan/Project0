using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex;

    public float strength = 5f;
    public float gravity = -9.81f;
    private Vector3 direction;

    public GraphicRaycaster raycaster;
    public EventSystem eventSystem;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;

        direction = Vector3.zero;
    }

    private void Update()
    {
        bool isDesktopInput = Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);
        bool isTouchInput = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;

        bool isTouchingUI = IsPointerOverUI();

        if ((isDesktopInput || isTouchInput) && !isTouchingUI)
        {
            direction = Vector3.up * strength;
        }

        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;
    }

    private bool IsPointerOverUI()
    {
        PointerEventData pointerData = new PointerEventData(eventSystem);

        if (Input.touchCount > 0)
        {
            pointerData.position = Input.GetTouch(0).position;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            pointerData.position = Input.mousePosition;
        }

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerData, results);

        return results.Count > 0;
    }

    private void AnimateSprite()
    {
        spriteIndex++;

        if (spriteIndex >= sprites.Length) 
        {
            spriteIndex = 0;
        }

        spriteRenderer.sprite = sprites[spriteIndex];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle") 
        {
            FindObjectOfType<GameManager>().GameOver();
        } 
        else if (other.gameObject.tag == "Scoring") 
        {
            FindObjectOfType<GameManager>().IncreaseScore();
        }
    }

}
