using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Launch(Vector2 direction, float speed, float lifetime, string pattern)
    {
        if (pattern == "Straight")
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.linearVelocity = direction * speed;
            Invoke("Deactivate", lifetime);
        }
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
