using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OrderObject 
{
    public Vector3 position;
    public GameObject gameStation;

    public OrderObject(Vector3 position, GameObject gameStation)
    {
        this.position = position;
        this.gameStation = gameStation;
    } 
}
