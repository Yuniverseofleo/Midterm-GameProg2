using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public SpriteRenderer spriteRenderer;
    public Color[] possibleColors = { Color.green, Color.red, Color.yellow };

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (spriteRenderer != null && possibleColors.Length > 0)
        {
            int index = Random.Range(0, possibleColors.Length);
            spriteRenderer.color = possibleColors[index];
        }
    }

    private void Update()
    {
        if (player == null) return;
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

        Vector2 dir = player.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Bullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null && ColorsMatch(spriteRenderer.color, bullet.bulletColor))
            {
                Destroy(gameObject);
                Destroy(other.gameObject);
            }
            else
            {
                Destroy(other.gameObject);
            }
        }
    }

    private bool ColorsMatch(Color a, Color b)
    {
        float tolerance = 0.01f;
        return Mathf.Abs(a.r - b.r) < tolerance &&
               Mathf.Abs(a.g - b.g) < tolerance &&
               Mathf.Abs(a.b - b.b) < tolerance;
    }
}
