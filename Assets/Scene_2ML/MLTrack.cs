using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

public class MLTrack : Agent
{
       
    [SerializeField] private Vector3 startPosition;
    public override void OnEpisodeBegin()
    {
        transform.position = startPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
