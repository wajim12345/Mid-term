using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


[RequireComponent(typeof(Camera))]
public class CameraMovementBehaviour : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;

    [Header("Player Turn")]
    [SerializeField] private float turnSpeed;
    [SerializeField] private bool invertMouse;

    private float camXRotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //lock cursor in center of gameview
        Cursor.visible = false; //make cursor invisible
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();
    }

    void RotateCamera()
    {
        
        camXRotation += Time.deltaTime * playerInput.mouseY * turnSpeed * (invertMouse ? 1 : -1); //if invertMouse is true then +1, if not -1
        camXRotation = Mathf.Clamp(camXRotation, -85f, 85f);

        transform.localRotation = Quaternion.Euler(camXRotation, 0, 0);
    }
}
