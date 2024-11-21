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

    public bool isReviving = false;

    public Color safeColor = Color.green; // สีเมื่อชนได้
    private Color originalColor;

    public float magnetRadius = 5f; // รัศมี Magnet
    public float magnetDuration = 5f; // ระยะเวลา Magnet ทำงาน
    private bool isMagnetActive = false; // สถานะ Magnet

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
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
        if (isReviving) return;

        bool isDesktopInput = Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);
        bool isTouchInput = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;

        bool isTouchingUI = IsPointerOverUI();

        if ((isDesktopInput || isTouchInput) && !isTouchingUI)
        {
            direction = Vector3.up * strength;
        }

        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;

        if (isMagnetActive)
        {
            AttractFoodItems(); // เรียกฟังก์ชันดูดไอเทมเมื่อ Magnet ทำงาน
        }
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

    public bool canCollideSafely = false;
    public float safeDuration = 5f;

    public void EnableSafeCollision()
    {
        canCollideSafely = true;
        Invoke(nameof(DisableSafeCollision), safeDuration);
        spriteRenderer.color = safeColor; // เปลี่ยนสีตัวละคร
    }

    private void DisableSafeCollision()
    {
        canCollideSafely = false;
        spriteRenderer.color = originalColor; // คืนสีเดิม
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            if (!canCollideSafely) // ถ้ายังไม่มีสถานะชนได้ -> จบเกม
            {
                FindObjectOfType<GameManager>().GameOver();
            }
        }
        else if (other.gameObject.tag == "Scoring") 
        {
            FindObjectOfType<GameManager>().IncreaseScore();
        }
        else if (other.gameObject.tag == "Shield") // เมื่อชนอาหารที่เป็นเกราะ
        {
            EnableSafeCollision(); // เปิดสถานะชนได้
            Destroy(other.gameObject); // ลบอาหารออก
        }
        else if (other.gameObject.tag == "Magnet") // เมื่อชนไอเทม Magnet
        {
            ActivateMagnet(); // เปิดใช้งาน Magnet
            Destroy(other.gameObject); // ลบไอเทม Magnet ออก
        }
    }

    // ฟังก์ชันเปิดใช้งาน Magnet
    private void ActivateMagnet()
    {
        isMagnetActive = true;
        Invoke(nameof(DeactivateMagnet), magnetDuration); // ปิด Magnet หลังเวลาที่กำหนด
    }

    // ฟังก์ชันปิด Magnet
    private void DeactivateMagnet()
    {
        isMagnetActive = false;
    }

    // ฟังก์ชันดูดไอเทม Food
    private void AttractFoodItems()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, magnetRadius); // ตรวจจับไอเทมในรัศมี
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Fish"))
            {
                collider.transform.position = Vector3.MoveTowards(collider.transform.position, transform.position, Time.deltaTime * 5f); // ดูดไอเทมเข้า
            }
            if (collider.CompareTag("Seaweed"))
            {
                collider.transform.position = Vector3.MoveTowards(collider.transform.position, transform.position, Time.deltaTime * 5f); // ดูดไอเทมเข้า
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // วาดรัศมี Magnet ใน Editor
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, magnetRadius);
    }
}
