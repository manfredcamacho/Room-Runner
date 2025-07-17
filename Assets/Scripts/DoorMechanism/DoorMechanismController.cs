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

    void Start()
    {
        leverA.OnLeverActivated.AddListener(onLeverActivated);
        leverB.OnLeverActivated.AddListener(onLeverActivated);
    }

    void onLeverActivated()
    {
        doorA.ToggleDoor();
        doorB.ToggleDoor();
        leverA.ToggleLever();
        leverB.ToggleLever();
        SoundManager.PlaySfx(SoundType.DOOR);
    }
}
