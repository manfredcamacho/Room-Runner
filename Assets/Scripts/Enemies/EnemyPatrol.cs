using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Configuración de Patrulla")]
    public float speed = 2f;
    public Transform pointA;
    public Transform pointB;

    private Transform currentTarget;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        currentTarget = pointB;
    }

    void Update()
    {
        if (pointA == null || pointB == null)
        {
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            if (currentTarget == pointB)
            {
                currentTarget = pointA;
            }
            else
            {
                currentTarget = pointB;
            }

            Flip();
        }
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}