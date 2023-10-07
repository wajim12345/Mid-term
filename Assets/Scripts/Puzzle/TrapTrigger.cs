using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrapTrigger : MonoBehaviour
{
    [SerializeField] UnityEvent TrapTrig;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TrapTrig?.Invoke();
            Destroy(gameObject);
        }
    }

    
}
