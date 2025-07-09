using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    [Header("Door configuration")]
    public Transform destinationDoor;

    private CameraController mainCamera; 
    private bool isPlayerInRange = false;
    private bool isOpen;
    private Animator animator;

    private void Start()
    {
        mainCamera = Camera.main.GetComponent<CameraController>();
        animator = GetComponent<Animator>();
        isOpen = animator.GetBool("isOpen");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    void Update()
    {
        if (isPlayerInRange && isOpen && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null && destinationDoor != null && mainCamera != null)
            {
                StartCoroutine(TeleportSequence(playerObject));
            }
            else
            {
                Debug.LogError("Falta asignar el destino, la cámara o el jugador no tiene el tag 'Player'");
            }
        }
    }

    private IEnumerator TeleportSequence(GameObject player)
    {
        player.SetActive(false);

        //Inicia la transición de la cámara y espera a que termine
        yield return StartCoroutine(mainCamera.TransitionToTarget(destinationDoor));

        player.transform.position = destinationDoor.position;
        player.SetActive(true);
    }
    public void ToggleDoor()
    {
        isOpen = !isOpen;
        animator.SetBool("isOpen", isOpen);
    }

    public void OpenDoor() { isOpen = true; animator.SetBool("isOpen", true); }
    public void CloseDoor() { isOpen = false; animator.SetBool("isOpen", false); }
}