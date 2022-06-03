using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    #region Variables

    public FlockAgent agentPrefab;
    public List<FlockAgent> agents;// = new List<FlockAgent>();
  //  public List<Transform> context;

    public FlockBehaviour behaviour;

    [Range(10, 500)]
    public int startingCount = 250;
    const float agentDensity = 0.08f;

    [Range(1, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighbourRadious = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    float _squareMaxSpeed;
    float _squareNeighborRadius;
    float _squareAvoidanceRadius;

    public float SquareAvoidanceRadius { get { return _squareAvoidanceRadius; } }

    #endregion


    private void Start()
    {
        _squareMaxSpeed = maxSpeed * maxSpeed;
        _squareNeighborRadius = neighbourRadious * neighbourRadious;
        _squareAvoidanceRadius = _squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < startingCount; i++)
        {
            FlockAgent newAgent = Instantiate(  //create a clone of gameobject or prefab
                agentPrefab,  //this is the prefab 
                Random.insideUnitCircle * startingCount * agentDensity, //
                Quaternion.Euler(Vector3.forward * Random.Range(0, 360f)), //translate the regular rotation (x,y,z) into a Quaternion Euler (x,y,z,w)
                transform
                );
            newAgent.name = "Agent " + i;
            //  newAgent

            agents.Add(newAgent);
        }

    }


    private void FixedUpdate()
    {
        foreach (FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);

            //FOR TESTING
           // agent.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, context.Count / 6f);

            Vector2 move = behaviour.CalculateMove(agent, context, this);
            move *= driveFactor;
            if (move.sqrMagnitude > _squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }

            agent.Move(move);
        }
    }



    private List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighbourRadious);
        foreach (Collider2D c in contextColliders)
        {
            if (c != agent.AgentCollier)
            {
                context.Add(c.transform);

            }
        }

        return context;

    }



}
