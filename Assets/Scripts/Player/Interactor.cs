using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactor : MonoBehaviour
{
    //protected is like private but it also allows any class that inheriting from it to have access.
    //private only allows this specific class to access

    protected PlayerInput playerInput;


    private void Awake()
    {
        playerInput = PlayerInput.GetInstance();
    }
    // Update is called once per frame
    void Update()
    {
        Interact();
    }

    public abstract void Interact();
}
