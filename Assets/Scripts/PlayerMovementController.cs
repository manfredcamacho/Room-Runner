using UnityEngine;

public class SimpleSlopeWalker : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float moveSpeed = 5f; // Velocidad de movimiento horizontal

    [Header("Referencias de Componentes")]
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer; // Para girar el sprite
    private Animator animator;

    private float horizontalInput;
    private bool isFacingRight = true;

    // Se llama una vez cuando el script se inicia
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        // Verificaciones para asegurar que los componentes existen
        if (rb == null)
        {
            Debug.LogError("¡Rigidbody2D no encontrado en el personaje! Asegúrate de añadirlo.");
            enabled = false; // Desactiva el script si falta el Rigidbody2D
            return;
        }
        if (spriteRenderer == null)
        {
            Debug.LogError("¡SpriteRenderer no encontrado en el personaje! Asegúrate de añadirlo.");
            // Podríamos desactivarlo también, pero el movimiento aún podría funcionar sin el sprite
        }
        if (animator == null)
        {
            Debug.LogError("!Animator no encontrado en el personaje! Asegúrate de añadirlo.");
        }
    }

    // Se llama cada frame
    void Update()
    {
        // --- Captura de Input ---
        // Input.GetAxisRaw devuelve -1, 0, o 1 para un input más directo
        horizontalInput = Input.GetAxisRaw("Horizontal"); // Funciona para A/D y Flechas Izquierda/Derecha

        if (horizontalInput == 0) 
            animator.SetInteger("movement", 0);
        else 
            animator.SetInteger("movement", 1); 


        // --- Lógica para Girar el Sprite ---
        if (horizontalInput > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0 && isFacingRight)
        {
            Flip();
           
        }
    }

    // Se llama en intervalos de tiempo fijos, ideal para la física
    void FixedUpdate()
    {
        // --- Aplicar Movimiento ---
        // Modificamos la velocidad horizontal del Rigidbody2D.
        // Mantenemos la velocidad vertical actual (rb.velocity.y) para que la gravedad siga funcionando
        // y para que el personaje se mantenga en el suelo y suba/baje pendientes correctamente.
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }

    // Método para girar el personaje
    void Flip()
    {
        isFacingRight = !isFacingRight;

        // Multiplica la escala local en X por -1 para girar el personaje
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }
}
