using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask detectionLayer;

    public UnityEvent OnCubePlaced;
    public UnityEvent OnCubeRemoved;

    public UnityEvent OnRobotEnter;
    public UnityEvent OnRobotLeave;

    private void OnCollisionEnter(Collision collision)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius, detectionLayer);
        Debug.Log(hitColliders.Length);

        foreach (var collider in hitColliders)
        {
            if(collider.CompareTag("PickCube"))
            {
                OnCubePlaced?.Invoke();
                break;
            }
            else if(collider.CompareTag("Robot"))
            {
                OnRobotEnter?.Invoke();
                Debug.Log("Robot Enter");
                break;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("PickCube"))
        {
            OnCubeRemoved?.Invoke();   
        }
        else if (collision.gameObject.CompareTag("Robot"))
        {
            OnRobotLeave?.Invoke();
        }
    }
}
