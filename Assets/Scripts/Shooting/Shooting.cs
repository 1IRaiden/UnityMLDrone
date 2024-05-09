using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    GameObject shoot;

    Transform transform_drone;

    void Awake()
    {
        transform_drone = GetComponent<Transform>();
    }

    public void Shoot(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            GameObject body = Instantiate(shoot, transform_drone.position, Quaternion.identity);
            Destroy(body, 0.5f);
        }
    }
}

