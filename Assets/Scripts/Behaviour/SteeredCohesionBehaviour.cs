using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenuAttribute(menuName = "Flock/Behaviour/Steered Cohesion")]
public class SteeredCohesionBehaviour : CohesionBehaviour
{

    private Vector2 currentVelocity;
    public float agentSmoothTime = 0.5f;


    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {

        //direction from a to b = b-a
        Vector2 cohesionMove = base.CalculateMove(agent, context, flock);
        cohesionMove = Vector2.SmoothDamp(agent.transform.up, cohesionMove, ref currentVelocity, agentSmoothTime);

        return cohesionMove;
    }



}
