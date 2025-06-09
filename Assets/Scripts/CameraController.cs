using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [Header("Seguimiento del Jugador")]
    public Transform playerTarget; // Arrastra el objeto del jugador aqu�
    public Vector3 offset; // Un posible desplazamiento (ej. si quieres la c�mara m�s arriba)

    // La variable smoothSpeed ya no es necesaria y se ha eliminado.

    [Header("Transici�n entre Puertas")]
    public float transitionSpeed = 8f; // Velocidad de la c�mara al viajar entre puertas

    private bool isTransitioning = false;

    // Usamos LateUpdate para el movimiento de la c�mara.
    void LateUpdate()
    {
        // Si no est� en transici�n, sigue al jugador.
        if (playerTarget != null && !isTransitioning)
        {
            // --- CORRECCI�N 2: SEGUIMIENTO R�GIDO ---
            // Se elimina Vector3.Lerp para un seguimiento instant�neo.
            Vector3 targetPosition = playerTarget.position + offset;
            // Se actualiza la posici�n directamente, manteniendo la Z de la c�mara.
            transform.position = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
        }
    }

    // Corrutina p�blica que puede ser llamada por el script de la puerta
    public IEnumerator TransitionToTarget(Transform destination)
    {
        isTransitioning = true;

        // --- CORRECCI�N 1: C�LCULO DE DESTINO IGNORANDO Z ---
        // Creamos un vector de destino que usa la X e Y de la puerta, 
        // pero mantiene la Z actual de la c�mara.
        Vector3 targetDestination = new Vector3(destination.position.x, destination.position.y, transform.position.z);

        // El bucle ahora compara la posici�n de la c�mara con el 'targetDestination' que tiene la Z correcta.
        while (Vector3.Distance(transform.position, targetDestination) > 0.1f)
        {
            // Mueve la c�mara hacia el destino Z-corregido.
            transform.position = Vector3.MoveTowards(transform.position, targetDestination, transitionSpeed * Time.deltaTime);
            yield return null; // Espera al siguiente frame
        }

        // Para asegurar la precisi�n, al final del bucle, ajustamos la c�mara a la posici�n exacta.
        transform.position = targetDestination;

        isTransitioning = false; // Desactivamos el modo transici�n.
    }
}