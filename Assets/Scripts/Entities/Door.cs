using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private MeshRenderer doorRenderer;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material detectedMaterial;
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private const float waitTIme = 1.0f;

    private bool isLocked = true;
    private float timer = 0;



    //when player enters the collider, changes door color to detected color
    private void OnTriggerEnter(Collider other)
    {
        if (!isLocked && other.CompareTag("Player"))
        {
            timer = 0;
            doorRenderer.material = detectedMaterial;

        }
    }
    //when player stays in the collider for more than 1 second, opens the door
    private void OnTriggerStay(Collider other)
    {
        if (isLocked)
        {
            return;
        }
        //if the gameobject in the collider is not player, return.
        if (!other.CompareTag("Player"))
        {
            return;
        }

        timer += Time.deltaTime;

        if (timer >= waitTIme)
        {
            timer = waitTIme;
            doorAnimator.SetBool("DoorControl", true);

            OpenDoor(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        doorAnimator.SetBool("DoorControl", false);
        doorRenderer.material = defaultMaterial;
        OpenDoor(false);
    }

    public void LockDoor()
    {
        isLocked = true;
    }

    public void UnlockDoor()
    {
        isLocked = false;
    }

    public void OpenDoor(bool state)
    {
        if(!isLocked)
        {
            doorAnimator.SetBool("Door", state);
        }
    }
}
