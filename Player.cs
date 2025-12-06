using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float rotateSpeed = 200f;
    public float shootInterval = 1f;
    public float bulletSpeed = 10f;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public SpriteRenderer spriteRenderer;

    private Color[] possibleColors = { Color.green, Color.red, Color.yellow };
    private Color currentColor;
    private float shootTimer;

    private void Start()
    {
        SetColor(possibleColors[0]);
    }

    private void Update()
    {
        HandleRotation();
        HandleShooting();
        HandleClickColorChange();
    }

    private void HandleRotation()
    {
        float rotationInput = 0f;
        if (Keyboard.current.aKey.isPressed) rotationInput = 1f;
        if (Keyboard.current.dKey.isPressed) rotationInput = -1f;
        transform.Rotate(0f, 0f, rotationInput * rotateSpeed * Time.deltaTime);
    }

    private void HandleShooting()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootInterval)
        {
            Shoot();
            shootTimer = 0f;
        }
    }

    private void Shoot()
    {
        if (bulletPrefab == null || firePoint == null) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = firePoint.up * bulletSpeed;

        SpriteRenderer bulletRenderer = bullet.GetComponent<SpriteRenderer>();
        if (bulletRenderer != null)
            bulletRenderer.color = currentColor;

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
            bulletScript.bulletColor = currentColor;
    }

    private void HandleClickColorChange()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            int index = Random.Range(0, possibleColors.Length);
            SetColor(possibleColors[index]);
        }
    }

    private void SetColor(Color color)
    {
        currentColor = color;
        if (spriteRenderer != null)
            spriteRenderer.color = color;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
            Debug.Log("Player collided with enemy!");
    }
}
