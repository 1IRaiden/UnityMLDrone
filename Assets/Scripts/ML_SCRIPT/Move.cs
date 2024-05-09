using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine.InputSystem;
using Unity.MLAgents.Policies;
using Unity.Barracuda;

public class Move : Agent
{
    [SerializeField]
    Transform targetposition;
    float moveSpeed = 1.1f;

    PlayerInputAction input;

    void Awake()
    {
        input = new PlayerInputAction();
        input.Player.Enable();
        // Debug.Log("Enable");
        // Debug.Log(targetposition.position);
        
    }


    public override void OnEpisodeBegin()
    {
        transform.position = new Vector3(0, 1, 0);
        // Debug.Log("Начался новый эпизод");
        // Debug.Log(Time.deltaTime);
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(targetposition.position);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.DiscreteActions[0]-1;
        float moveY = actions.DiscreteActions[1]-1;
        float moveZ = actions.DiscreteActions[2]-1;
        // Debug.Log(moveX.ToString() + " " + moveY.ToString() + " " + moveZ.ToString()) ;
        transform.position += new Vector3(moveX, moveY, moveZ)*moveSpeed* Time.deltaTime;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;

        discreteActions[0] = (int)input.Player.Movement.ReadValue<Vector3>().x+1;
        discreteActions[1] = (int)input.Player.Movement.ReadValue<Vector3>().y+1;
        discreteActions[2] = (int)input.Player.Movement.ReadValue<Vector3>().z+1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Goal>(out Goal goal)) {
            AddReward(20f);  
            EndEpisode();
            Debug.Log("revard+");
        }
        else if (other.TryGetComponent<NotHere>(out NotHere notHere)){
            AddReward(-3f);
            EndEpisode();
            Debug.Log("revard-");
                
        }

    }

}
