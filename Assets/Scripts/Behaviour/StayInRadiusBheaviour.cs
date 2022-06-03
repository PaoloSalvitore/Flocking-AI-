using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenuAttribute(menuName = "Flock/Behaviour/Stay In Radius")]
public class StayInRadiusBehaviour : FlockBehaviour
{

    [SerializeField] private Vector2 _center;
    [SerializeField] private float _radius = 15f;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //direction to the center
        Vector2 centerOffset = _center - (Vector2)agent.transform.position;


        float t = centerOffset.magnitude / _radius;
        
        if (t < 0.9f) //if we are between the center and 90% of the radius
        {
            return Vector2.zero;
        }

        return centerOffset * t * t;

    }
}
