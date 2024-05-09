using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class CreatorWorld : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Это ваша зарядная площадка")]
    private GameObject station;

    [SerializeField]
    [Tooltip("Это ваша стартовая площадка")]
    private GameObject startSquare;

    [SerializeField]    
    [Tooltip("Это ваш Box")]
    private GameObject box_obj;

    [SerializeField]
    private GameObject drone;


    // Recharge
    public Vector3 position_recharge_1;
    public Vector3 position_recharge_2;

    StationRecharge stationRecharge_1;
    StationRecharge stationRecharge_2;

    // Spawn
    [SerializeField]
    Vector3[] vector3s;
    startSquare[] StartSquares;

    // Box
    float time = 3;
    GameObject obj;
    Box box;

    // Position drone
    [SerializeField]
    Transform drone_tranform;

    [SerializeField]
    Dictionary<string, Vector3> spawn = new Dictionary<string, Vector3>();
    bool isTaken = false;
    float distance;
    float distance_spawn;

    void Awake()
    {
        int length = vector3s.Length;
        StartSquares = new startSquare[length];

        stationRecharge_1 = new StationRecharge(position_recharge_1, station);
        stationRecharge_2 = new StationRecharge(position_recharge_2, station);

        for (int i = 0; i < length; i++)
        {
            StartSquares[i] = new startSquare(vector3s[i], startSquare);
        }

        // Временное решение:
        spawn.Add(drone.name, vector3s[0]);

        box = new Box(new Vector3(0,0,0), box_obj);
    }

    void Start()
    {
        Instantiate(stationRecharge_1.gameStation, stationRecharge_1.position, Quaternion.identity);
        Instantiate(stationRecharge_2.gameStation, stationRecharge_2.position, Quaternion.identity);

        foreach (startSquare startsquare in StartSquares)
        {
            Instantiate(startsquare.gameStation, startsquare.position, Quaternion.identity);
        }
    }


    void Update()
    {
        if (obj == null)
        {
            time = time - Time.deltaTime;
            if (time < 0)
            {
                time = 3;
                Vector3 datatransform = getRandomVector();
                box.position = datatransform;
                obj = Instantiate(box.gameStation, box.position, Quaternion.identity);
            }
        }
        else
        {
            if (!isTaken && Movement_system.onSurface)
            {
                distance = Vector3.Distance(drone_tranform.position, obj.transform.position);
                if (distance < 1)
                {
                    isTaken = true;
                    obj.transform.SetParent(drone_tranform);
                    obj.transform.SetLocalPositionAndRotation(new Vector3(0, -(float)0.3, 0), Quaternion.identity);
                }
            }
            else if(isTaken && Movement_system.onSurface)
            {
                distance_spawn = Vector3.Distance(drone_tranform.position, spawn[drone.name]);
                if (distance_spawn < 1)
                    Destroy(obj);
            }
        }


    }

    Vector3 getRandomVector()
    {
       float x = Random.Range(-5.0f, 5.0f);
       float z = Random.Range(-5.0f, 5.0f);
       Vector3 vector3 = new Vector3(x, (float)0.1, z);
       return vector3;
    }

}
