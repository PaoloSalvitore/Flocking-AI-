using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/AvoidPlayer")]

public class AvoidPlayerBehaviour : FlockBehaviour
{
    [SerializeField] float _avoidPlayerRadius = 40f;
    float _squarePlayerRadius = -1;
    GameObject player;

    void FindPlayer()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerMovement>().gameObject;
        }
    }

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        FindPlayer();
        if(_squarePlayerRadius < 0)
        {
            _squarePlayerRadius = _avoidPlayerRadius * _avoidPlayerRadius;

        }

        if(player == null)
        {
            return Vector2.zero;
        }

        Vector2 avoidanceMove = Vector2.zero;

        if (Vector2.SqrMagnitude(player.transform.position - agent.transform.position) < _squarePlayerRadius)
        {
            avoidanceMove += (Vector2)(agent.transform.position - player.transform.position);

        }
       
        return avoidanceMove.normalized;
    }

}
