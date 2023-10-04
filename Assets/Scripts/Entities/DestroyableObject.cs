using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//one class can only inherit one class, but can inherit multiple interface
//need to implement all method from an interface
public class DestroyableObject : MonoBehaviour, IDestroyable
{
    public void OnCollided()
    {
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
