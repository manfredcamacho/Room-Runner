using UnityEngine;

public class SimpleSlopeWalker : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float moveSpeed = 5f; 

    [Header("Referencias de Componentes")]
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private float horizontalInput;
    private bool isFacingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        if (rb == null)
        {
            Debug.LogError("¡Rigidbody2D no encontrado en el personaje! Asegúrate de añadirlo.");
            enabled = false;
            return;
        }
        if (spriteRenderer == null)
        {
            Debug.LogError("¡SpriteRenderer no encontrado en el personaje! Asegúrate de añadirlo.");
        }
        if (animator == null)
        {
            Debug.LogError("!Animator no encontrado en el personaje! Asegúrate de añadirlo.");
        }
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput == 0) 
            animator.SetInteger("movement", 0);
        else 
            animator.SetInteger("movement", 1); 

        if (horizontalInput > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0 && isFacingRight)
        {
            Flip();
           
        }
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }
}
