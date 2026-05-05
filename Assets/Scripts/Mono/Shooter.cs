using UnityEngine;
using UnityEngine.Rendering;

public class Shooter : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] Shooting shootData;
    [SerializeField] Transform shootPoint;
    [SerializeField] ObjectPooler objectPooler;
    private GameObject bulletObject;
    private float timeSinceLastShot;
    float desiredAngle;

    Vector2 mousePos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 lookdir = mousePos - rb.position;
        desiredAngle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg - 90f;
        timeSinceLastShot = shootData.fireRate; // Allow shooting immediately at the start
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 lookdir = mousePos - rb.position;
        desiredAngle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg - 90f;
        
        timeSinceLastShot += Time.deltaTime;

        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        rb.MoveRotation(desiredAngle);
    }

    private void Shoot()
    {
        float timeBetweenShots = 1f / shootData.fireRate;
        if (timeSinceLastShot >= timeBetweenShots)
        {
            GameObject bulletObject = objectPooler.GetPooledObject();
            bulletObject.transform.position = shootPoint.position;
            bulletObject.transform.rotation = shootPoint.rotation;
            bulletObject.SetActive(true);
            Bullet script = bulletObject.GetComponent<Bullet>();
            script.Launch(shootPoint.up, shootData.fireForce, shootData.lifetime, shootData.bulletPattern);

            timeSinceLastShot = 0f;
        }
        

    }
}
