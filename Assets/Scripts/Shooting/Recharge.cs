using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading;
using UnityEngine.UIElements;

public class Recharge : MonoBehaviour
{
    [SerializeField]
    GameObject creator;

    Transform position;
    CreatorWorld creatorWorld;
    Movement_system movement_System;

    public static bool isRecharge = false;

    void Awake()
    {
        creatorWorld = creator.GetComponent<CreatorWorld>();
        position = GetComponent<Transform>();
        movement_System = GetComponent<Movement_system>();
    }

    public InputActionReference action;
    public void Recharge_Station(InputAction.CallbackContext obj)
    {
        Debug.Log(Vector3.Distance(creatorWorld.position_recharge_2, position.position));
        Debug.Log(Vector3.Distance(creatorWorld.position_recharge_1, position.position));
        if ((Vector3.Distance(creatorWorld.position_recharge_2, position.position) < 1 ||
            Vector3.Distance(creatorWorld.position_recharge_1, position.position) < 1) && (Movement_system.onSurface))
        {
            if (obj.performed)
            {
                Debug.Log("Зарядка началась");
                movement_System.startCoroutine_();      

            }
        }
    }

}
