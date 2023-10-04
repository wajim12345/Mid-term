using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private bool invertMouse;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float sprintMultiplier;

    [Header("Ground Checker")]
    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;

    [Header("Player Shoot")]
    [SerializeField] Rigidbody bulletPrefab;
    [SerializeField] Rigidbody rocketPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float shootForce;

    [Header("Interact")]
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask buttonLayer;
    [SerializeField] private float interactionDistance;

    [Header("Pick And Drop")]
    [SerializeField] private LayerMask pickupLayer;
    [SerializeField] private float pickupDistance;
    [SerializeField] private Transform attachtransform;


    private float horizontal, vertical;
    private float mouseX, mouseY;

    private CharacterController characterController;

    //Rayccast
    private RaycastHit raycastHit;
    private ISelectable selectable;

    //Pick and Drop
    private bool isPicked = false;
    private IPickable pickable;

    private float camXRotation;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private float moveMultiplier = 1;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; //lock cursor in center of gameview
        Cursor.visible = false; //make cursor invisible
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        MovePlayer();
        RotatePlayer();
        GroundCheck();
        JumpCheck();

        ShootBullet();
        ShootRocket();
        Interact();
        PickAndDrop();
    }

    void GetInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        moveMultiplier = Input.GetButton("Sprint") ? sprintMultiplier : 1; // Using ternary operation. if sprint button is pressed down, use sprint multiplier, if not, use 1;
    }

    void MovePlayer()
    {
        characterController.Move((transform.forward * vertical + transform.right * horizontal) * moveSpeed * Time.deltaTime * moveMultiplier);

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        //v(velocity) = u (initial velocity)+ a(gravity)t(Time)
        //v = 1/2 at^2
        playerVelocity.y += gravity * Time.deltaTime; // apply gravity to player

        characterController.Move(playerVelocity * Time.deltaTime);

    }

    void RotatePlayer()
    {
        transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime * mouseX);

        camXRotation += Time.deltaTime * mouseY * turnSpeed * (invertMouse ? 1 : -1); //if invertMouse is true then +1, if not -1
        camXRotation = Mathf.Clamp(camXRotation, -85f, 85f);

        cameraTransform.localRotation = Quaternion.Euler(camXRotation, 0, 0);
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheckPos.position, groundCheckRadius, groundLayer);
    }

    void JumpCheck()
    {
        if (Input.GetButtonDown("Jump"))
        {
            playerVelocity.y = jumpVelocity;
        }
    }

    void ShootBullet()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Rigidbody bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            bullet.AddForce(shootPoint.forward * shootForce);
            Destroy(bullet.gameObject, 5f);
        }

    }

    void ShootRocket()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Rigidbody rocket = Instantiate(rocketPrefab, shootPoint.position, shootPoint.rotation);
            rocket.AddForce(shootPoint.forward * shootForce);
            Destroy(rocket.gameObject, 5f);
        }
    }

    void Interact()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out raycastHit, interactionDistance, buttonLayer))
        {
            selectable = raycastHit.transform.GetComponent<ISelectable>();

            if (selectable != null)
            {
                selectable.OnHoverEnter();

                if (Input.GetKeyDown(KeyCode.E))
                {
                    selectable.OnSelect();
                }
            }
        }
        if (raycastHit.transform == null && selectable != null)
        {
            selectable.OnHoverExit();
            selectable = null;
        }
    }

    void PickAndDrop()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out raycastHit, pickupDistance, pickupLayer))
        {
            if (Input.GetKeyDown(KeyCode.E) && !isPicked)
            {
                pickable = raycastHit.transform.GetComponent<IPickable>();

                if (pickable == null)
                {
                    return;
                }

                pickable.OnPicked(attachtransform);
                isPicked = true;
                return;
            }

        }

        if (Input.GetKeyDown(KeyCode.E) && isPicked && pickable != null)
        {
            Debug.Log("item dropped");
            pickable.OnDropped();
            isPicked = false;
        }
    }
}
