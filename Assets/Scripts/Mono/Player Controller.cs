using UnityEngine;
using System.Collections;

// Top Down Player Controller for 2D games. This script allows the player to move in four directions (up, down, left, right) using the arrow keys or WASD keys. The movement speed can be adjusted using the moveSpeed variable.
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] Camera cam;

    [SerializeField] private float dodgeSpeed = 10f;
    [SerializeField] private float dodgeDuration = 0.2f;
    [SerializeField] private float dodgeCooldown = 1f;


    Rigidbody2D rb;

    public Animator anim;
    private SpriteRenderer sp;

    private bool isDodging = false;
    private bool canDodge = true;

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
        if (isDodging)
        {
            return;
        }

        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if (input.x < 0)
            sp.flipX = true;
        else if (input.x > 0)
            sp.flipX = false;

        if (input.x != 0f || input.y != 0f)
        {
            lastInputDir = input.normalized;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canDodge)
        {
            StartCoroutine(Dodge());
        }
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
        if (isDodging)
        {
            return;
        }

        rb.linearVelocity = input.normalized * moveSpeed;
    }

    private IEnumerator Dodge()
    {
        isDodging = true;
        canDodge = false;
        Vector2 dodgeDirection = input.normalized;
        if (dodgeDirection == Vector2.zero)
        {
            dodgeDirection = lastInputDir;
        }
        float timer = 0f;

        while (timer < dodgeDuration)
        {
            Debug.Log("Dodging...");
            rb.linearVelocity = dodgeDirection * dodgeSpeed;
            timer += Time.deltaTime;
            yield return null;
        }
        isDodging = false;
        yield return new WaitForSeconds(dodgeCooldown);
        canDodge = true;
    }

}
