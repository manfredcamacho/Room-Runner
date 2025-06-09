using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Configuración de Patrulla")]
    public float speed = 2f; // Velocidad de movimiento del enemigo
    public Transform pointA; // Punto A del recorrido (arrastrar objeto aquí)
    public Transform pointB; // Punto B del recorrido (arrastrar objeto aquí)

    private Transform currentTarget; // El punto hacia el que se dirige actualmente
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Obtenemos el componente SpriteRenderer para poder girarlo
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("El enemigo no tiene un componente SpriteRenderer.");
        }

        // El enemigo empieza moviéndose hacia el punto B
        currentTarget = pointB;
    }

    void Update()
    {
        // Si no hemos definido los puntos, no hacemos nada para evitar errores.
        if (pointA == null || pointB == null)
        {
            return;
        }

        // Movemos al enemigo hacia el objetivo actual
        // Vector2.MoveTowards es una forma sencilla de mover un objeto a un punto
        transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);

        // Comprobamos si el enemigo ha llegado al objetivo
        // Usamos una distancia muy pequeña (0.1f) para considerarlo "llegado"
        if (Vector2.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            // Si llegó al objetivo, cambiamos al otro punto
            if (currentTarget == pointB)
            {
                currentTarget = pointA;
            }
            else
            {
                currentTarget = pointB;
            }

            // Una vez cambiado el objetivo, giramos el sprite
            Flip();
        }
    }

    // Método para girar el sprite en el eje X
    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}