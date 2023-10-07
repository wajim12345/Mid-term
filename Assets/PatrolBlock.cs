using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PatrolBlock : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform[] point;
    private int index = 0;
    private int indexChangeValue = 1;


    private void Update()
    {
        Patrol();
    }
    private void Patrol()
    {
        Transform nextPoint = point[index];

        transform.position = Vector3.MoveTowards(transform.position, nextPoint.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, nextPoint.position)< 0.2f)
        {
            if (index == point.Length-1)
            {
                indexChangeValue = -1;
            }
            if (index == 0)
            {
                indexChangeValue = 1;
            }
            index += indexChangeValue;
        }
    }
}
