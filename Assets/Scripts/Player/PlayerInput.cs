using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ensure this script is executed first
[DefaultExecutionOrder(-100)]
public class PlayerInput : MonoBehaviour
{
    public float horizontal { get; private set; }
    public float vertical { get; private set; }
    public float mouseX { get; private set; }
    public float mouseY { get; private set; }

    public bool sprintHeld { get; private set; }
    public bool jumpPressed { get; private set; }
    public bool interactPressed { get; private set; }
    public bool primaryFire { get; private set; }
    public bool secondaryFire { get; private set; }
    public bool weapon1Selected { get; private set; }
    public bool weapon2Selected { get; private set; }
    public bool commandPressed { get; private set; }
    private bool clear;

    //Singleton
    private static PlayerInput instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            //Destroy(instance.gameObject); this will destroy gameobject with script
            Destroy(instance);
        }

        instance = this;

    }

    public static PlayerInput GetInstance()
    {
        return instance;
    }

    // Update is called once per frame
    void Update()
    {
        ClearInput();
        ProcessInput();
    }

    void ProcessInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        sprintHeld = sprintHeld || Input.GetButton("Sprint");
        jumpPressed = jumpPressed || Input.GetButtonDown("Jump");
        interactPressed = interactPressed || Input.GetKeyDown(KeyCode.E);
        primaryFire = primaryFire || Input.GetButtonDown("Fire1");
        secondaryFire = secondaryFire || Input.GetButtonDown("Fire2");
        weapon1Selected = weapon1Selected || Input.GetKeyDown(KeyCode.Alpha1);
        weapon2Selected = weapon2Selected || Input.GetKeyDown(KeyCode.Alpha2);
        commandPressed = commandPressed || Input.GetKeyDown(KeyCode.F);
        

    }
    //FixedUpdate is usually used for physics of objects
    private void FixedUpdate()
    {
        clear = true;
    }

    void ClearInput()
    {
        if (!clear)
        {
            return;
        }

        horizontal = 0;
        vertical = 0;
        mouseX = 0;
        mouseY = 0;

        sprintHeld = false;
        jumpPressed = false;
        interactPressed = false;

        primaryFire = false;
        secondaryFire = false;

        weapon1Selected = false;
        weapon2Selected = false;

        commandPressed = false;

    }
}
