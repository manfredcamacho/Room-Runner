using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMechanism : MonoBehaviour
{

    [Header("Door Mechanism configuration")]
    [SerializeField] private DoorController doorA;
    [SerializeField] private LeverController leverA;
    [SerializeField] private DoorController doorB;
    [SerializeField] private LeverController leverB;

    private AudioSource audioSrc;

    void Start()
    {
        leverA.OnLeverActivated.AddListener(onLeverActivated);
        leverB.OnLeverActivated.AddListener(onLeverActivated);
        audioSrc = GetComponent<AudioSource>();
    }

    void onLeverActivated()
    {
        doorA.ToggleDoor();
        doorB.ToggleDoor();
        leverA.ToggleLever();
        leverB.ToggleLever();
        audioSrc.Play();
    }
}
