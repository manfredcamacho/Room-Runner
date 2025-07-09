using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeverController : MonoBehaviour
{
    [SerializeField] private UnityEvent onLeverActivated;
    public UnityEvent OnLeverActivated => onLeverActivated; //Sugar syntax getter

    private bool isPlayerInRange = false;
    private bool isOpen;
    private Animator animator;

    private void Awake()
    {
        if (onLeverActivated == null) onLeverActivated = new UnityEvent();
    }

    private void Start()
    {
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
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            onLeverActivated.Invoke();
        }
    }

    public void ToggleLever()
    {
        isOpen = !isOpen;
        animator.SetBool("isOpen", isOpen);
    }
}
