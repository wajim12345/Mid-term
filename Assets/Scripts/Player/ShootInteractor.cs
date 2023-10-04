using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootInteractor : Interactor
{
    [SerializeField] private Input inputType;

    [Header("Gun")]
    public MeshRenderer gunRenderer;
    public Color bulletColour;
    public Color rocketColour;

    [Header("Player Shoot")]
    //[SerializeField] Rigidbody bulletPrefab;
    public ObjectPool bulletPool;
    public ObjectPool rocketPool;

    [SerializeField] private Transform shootPoint;
    [SerializeField] private float shootForce;
    [SerializeField] private PlayerMovementBehaviour moveBehaviour;

    private float finalShootVelocity;
    private IShootStrategy currentShootStrategy;
    /*public enum Input
    {
        Primary,
        Secondary
    }*/


    // Update is called once per frame
    void Update()
    {
        Interact();
    }

    public override void Interact()
    {
        if(currentShootStrategy == null) 
        {
            currentShootStrategy = new BulletStrategy(this);
        }

        //change strategy
        if(playerInput.weapon1Selected)
        {
            currentShootStrategy = new BulletStrategy(this); 
        }
        if(playerInput.weapon2Selected)
        {
            currentShootStrategy= new RocketStrategy(this);
        }

        if (playerInput.primaryFire && currentShootStrategy != null)
        {
            currentShootStrategy.Shoot();
        }
    }

    void Shoot()
    {
        

        PooledObject pooledBullet = bulletPool.GetPooledObject();
        pooledBullet.gameObject.SetActive(true);

        //Rigidbody bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody bullet = pooledBullet.GetComponent<Rigidbody>();
        bullet.transform.position = shootPoint.position;
        bullet.transform.rotation = shootPoint.rotation;

        bullet.velocity = shootPoint.forward * finalShootVelocity;
        //Destroy(bullet.gameObject, 5f);

        bulletPool.DestroyPooledObject(pooledBullet, 5.0f);

    }

    public float GetShootVelocity()
    {
        finalShootVelocity = moveBehaviour.GetForwardSpeed() + shootForce;
        return finalShootVelocity;
    }

    public Transform GetShootPoint()
    {
        return shootPoint;
    }
}
