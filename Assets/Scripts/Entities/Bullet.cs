using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collided with {collision.gameObject.name}");

        IDestroyable destroyable = collision.gameObject.GetComponent<IDestroyable>();

        //check if destroyable game object exist
        if (destroyable != null)
        {
            destroyable.OnCollided();
        }

        //this destroy the bullet
        Destroy(gameObject);
    }
}
