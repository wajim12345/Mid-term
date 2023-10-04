using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour, IPickable
{
    Rigidbody cubeRb;


    void Start()
    {
        cubeRb = GetComponent<Rigidbody>();
    }
    
    public void OnDropped()
    {
        cubeRb.isKinematic = false;
        cubeRb.useGravity = true;
        transform.SetParent(null);
    }

    public void OnPicked(Transform attachTransform)
    {
        transform.position = attachTransform.position;
        transform.rotation = attachTransform.rotation;
        transform.SetParent(attachTransform);

        cubeRb.isKinematic = true;
        cubeRb.useGravity = false;
    }

    
    
}
