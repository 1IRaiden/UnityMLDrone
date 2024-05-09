using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class ManagerUINetcode : MonoBehaviour
{
    [SerializeField]
    private Button buttonSrv;

    [SerializeField]
    private Button buttonHost;

    [SerializeField]
    private Button buttonClnt;


    void Awake()
    {
        buttonSrv.onClick.AddListener(() => NetworkManager.Singleton.StartServer());
        buttonHost.onClick.AddListener(() => NetworkManager.Singleton.StartHost());
        buttonClnt.onClick.AddListener(() => NetworkManager.Singleton.StartClient());
    }

}
