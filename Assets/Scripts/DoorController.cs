using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    [Header("Configuraci�n de la Puerta")]
    public Transform destinationDoor; // Arrastra la puerta de destino aqu�

    [Header("Referencias")]
    public CameraController mainCamera; // Arrastra tu c�mara principal aqu�

    private bool isPlayerInRange = false;

    // Se activa cuando otro Collider2D entra en el Trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Comprobamos si el objeto que entr� es el jugador (usando la etiqueta "Player")
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    // Se activa cuando el Collider2D sale del Trigger
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    // Update se llama una vez por frame
    void Update()
    {
        // Si el jugador est� en el rango Y PRESIONA la tecla W o la Flecha Arriba...
        // Usamos GetKeyDown para que se active solo una vez al pulsar, y no continuamente.
        if (isPlayerInRange && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            // Buscamos el objeto del jugador para pasarlo a la corrutina
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null && destinationDoor != null && mainCamera != null)
            {
                StartCoroutine(TeleportSequence(playerObject));
            }
            else
            {
                Debug.LogError("Falta asignar el destino, la c�mara o el jugador no tiene el tag 'Player'");
            }
        }
    }

    private IEnumerator TeleportSequence(GameObject player)
    {
        // 1. Desactiva al jugador para que desaparezca
        player.SetActive(false);

        // 2. Inicia la transici�n de la c�mara y espera a que termine
        yield return StartCoroutine(mainCamera.TransitionToTarget(destinationDoor));

        // 3. Una vez la c�mara ha llegado, teletransporta al jugador a la puerta de destino
        // Se puede a�adir un peque�o offset para que no aparezca justo en el centro
        player.transform.position = destinationDoor.position;

        // 4. Reactiva al jugador
        player.SetActive(true);
    }
}