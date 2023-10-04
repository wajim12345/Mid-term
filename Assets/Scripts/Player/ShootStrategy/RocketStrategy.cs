using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketStrategy : IShootStrategy
{
    ShootInteractor _interactor;
    Transform shootPoint;
    //create a constructor that connect rocket strategy to shoot interactor script
    public RocketStrategy(ShootInteractor interactor)
    {
        Debug.Log("Switched to Rocket mode");
        _interactor = interactor;
        shootPoint = interactor.GetShootPoint();

        _interactor.gunRenderer.material.color = _interactor.rocketColour;
    }
    public void Shoot()
    {
        PooledObject pooledBullet = _interactor.rocketPool.GetPooledObject();
        pooledBullet.gameObject.SetActive(true);

        //Rigidbody rocket = Instantiate(rocketPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody bullet = pooledBullet.GetComponent<Rigidbody>();
        bullet.transform.position = shootPoint.position;
        bullet.transform.rotation = shootPoint.rotation;

        bullet.velocity = shootPoint.forward * _interactor.GetShootVelocity();
        //Destroy(rocket.gameObject, 5f);

        _interactor.rocketPool.DestroyPooledObject(pooledBullet, 5.0f);
    }
}
