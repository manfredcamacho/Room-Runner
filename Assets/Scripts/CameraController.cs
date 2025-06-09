using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [Header("Seguimiento del Jugador")]
    public Transform playerTarget; // Arrastra el objeto del jugador aquí
    public Vector3 offset; // Un posible desplazamiento (ej. si quieres la cámara más arriba)

    // La variable smoothSpeed ya no es necesaria y se ha eliminado.

    [Header("Transición entre Puertas")]
    public float transitionSpeed = 8f; // Velocidad de la cámara al viajar entre puertas

    private bool isTransitioning = false;

    // Usamos LateUpdate para el movimiento de la cámara.
    void LateUpdate()
    {
        // Si no está en transición, sigue al jugador.
        if (playerTarget != null && !isTransitioning)
        {
            // --- CORRECCIÓN 2: SEGUIMIENTO RÍGIDO ---
            // Se elimina Vector3.Lerp para un seguimiento instantáneo.
            Vector3 targetPosition = playerTarget.position + offset;
            // Se actualiza la posición directamente, manteniendo la Z de la cámara.
            transform.position = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
        }
    }

    // Corrutina pública que puede ser llamada por el script de la puerta
    public IEnumerator TransitionToTarget(Transform destination)
    {
        isTransitioning = true;

        // --- CORRECCIÓN 1: CÁLCULO DE DESTINO IGNORANDO Z ---
        // Creamos un vector de destino que usa la X e Y de la puerta, 
        // pero mantiene la Z actual de la cámara.
        Vector3 targetDestination = new Vector3(destination.position.x, destination.position.y, transform.position.z);

        // El bucle ahora compara la posición de la cámara con el 'targetDestination' que tiene la Z correcta.
        while (Vector3.Distance(transform.position, targetDestination) > 0.1f)
        {
            // Mueve la cámara hacia el destino Z-corregido.
            transform.position = Vector3.MoveTowards(transform.position, targetDestination, transitionSpeed * Time.deltaTime);
            yield return null; // Espera al siguiente frame
        }

        // Para asegurar la precisión, al final del bucle, ajustamos la cámara a la posición exacta.
        transform.position = targetDestination;

        isTransitioning = false; // Desactivamos el modo transición.
    }
}