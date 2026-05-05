using UnityEngine;

// Top Down Player Controller for 2D games. This script allows the player to move in four directions (up, down, left, right) using the arrow keys or WASD keys. The movement speed can be adjusted using the moveSpeed variable.
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] Camera cam;
    
    Rigidbody2D rb;

    public Animator anim;
    private SpriteRenderer sp;

    Vector2 input;
    Vector2 lastInputDir;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if (input.x < 0)
            sp.flipX = true;
        else if (input.x > 0)
            sp.flipX = false;
        /*
        //Animator Stuff
        anim.SetFloat("Horizontal", input.x);
        anim.SetFloat("Vertical", input.y);

        if (input.x != 0 || input.y != 0)
        {
            lastInputDir = new Vector2(input.x, input.y).normalized;
            anim.SetFloat("Horizontal", lastInputDir.x);
            anim.SetFloat("Vertical", lastInputDir.y);
        }*/

    }
    void FixedUpdate()
    {
        rb.linearVelocity = input.normalized * moveSpeed;
        
    }
}
