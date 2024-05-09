using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class NetMovement : NetworkBehaviour
{
    public static bool onSurface = false;

    Recharge recharge;

    PlayerInputAction playerAction;
    Shooting shooting;

    Transform transform_blue;

    [SerializeField]
    Transform rotation_object;

    //[SerializeField]
    //GameObject obj;

    [SerializeField]
    float order_speed = 5f;

    [SerializeField]
    float rotateSpeed = 50.0f;

    Quaternion startRotate;
    Quaternion newRotate;

    void Awake()
    {
        shooting = GetComponent<Shooting>();
        recharge = GetComponent<Recharge>();
    }

    void Start()
    {
        transform_blue = GetComponent<Transform>();
        GetComponent<Transform>().position = new Vector3(0, 2, 0);
        playerAction = new PlayerInputAction();
        playerAction.Player.Enable();

        playerAction.Player.Shoot.performed += shooting.Shoot;
        playerAction.Player.Recharge.performed += recharge.Recharge_Station;
        playerAction.Player.Takeoff.performed += Takeoff;
        playerAction.Player.Boarding.performed += Boarding;
    }

    void FixedUpdate()
    {    
        Vector3 direction = playerAction.Player.Movement.ReadValue<Vector3>();
        Vector3 world_vector = transform_blue.TransformVector(direction);

        if (IsOwner)
            rotation_object.position += world_vector * Time.deltaTime * order_speed;

        float quate = playerAction.Player.Rotation.ReadValue<float>();

        if (IsOwner)
        {
            if (quate != 0)
            {
                startRotate = rotation_object.rotation;
                float angle = rotateSpeed * Time.deltaTime * quate;
                newRotate = Quaternion.AngleAxis(angle, Vector3.up);

                Quaternion finalRotation = startRotate * newRotate;
                rotation_object.rotation = finalRotation;
            }
        }
        
    }

    void Boarding(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            if (!onSurface)
            {
                Debug.Log("������� � ���������� ���� �������");
                rotation_object.position = new Vector3(rotation_object.position.x, 0, rotation_object.position.z);
                onSurface = true;
                DisableInput();
            }
        }
    }

    void Takeoff(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            if (onSurface)
            {
                Debug.Log("����� � ��������� ���� �������");
                rotation_object.position = new Vector3(rotation_object.position.x, (float)0.1, rotation_object.position.z);
                onSurface = false;
                EnableInput();
            }
        }
    }

    void DisableInput()
    {
        playerAction.Disable();
        playerAction.Player.Takeoff.Enable();
        playerAction.Player.Recharge.Enable();
    }
    public void startCoroutine_()
    {
        Debug.Log("Im here");
        StartCoroutine("goTime");
    }

    public IEnumerator goTime()
    {
        playerAction.Player.Takeoff.Disable();
        playerAction.Player.Rotation.Enable();
        Debug.Log("�������� ��������");
        yield return new WaitForSeconds(10);
        Debug.Log("� ��� ������");
        playerAction.Player.Takeoff.Enable();
    }

    void EnableInput()
    {
        playerAction.Enable();
    }
}
