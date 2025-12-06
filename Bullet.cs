using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Color bulletColor;
    public float lifeTime = 3f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            SpriteRenderer enemyRenderer = other.GetComponent<SpriteRenderer>();
            if (enemyRenderer != null)
            {
                if (enemyRenderer.color == bulletColor)
                {
                    Destroy(other.gameObject); 
                    Destroy(gameObject);       
                }
                else
                {

                    Destroy(gameObject);
                }
            }
        }
    }
}
