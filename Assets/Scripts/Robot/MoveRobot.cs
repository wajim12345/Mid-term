using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRobot : MonoBehaviour
{
    [SerializeField] Transform targetPosition;
 
    public void MoveToTargetLocation()
    {
        if (targetPosition != null)
        {
            transform.position = targetPosition.position;
        }
    }

}
