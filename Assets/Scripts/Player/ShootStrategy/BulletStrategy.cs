using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStrategy : IShootStrategy
{
    ShootInteractor _interactor;
    Transform shootPoint;
    //connect bulletstrategy to shoot interactor script, to allow access from shoot interactor
    public BulletStrategy(ShootInteractor interactor)
    {
        Debug.Log("Switched to Bullet mode");
        _interactor = interactor;
        shootPoint = interactor.GetShootPoint();

        _interactor.gunRenderer.material.color = _interactor.bulletColour;
    }
    public void Shoot()
    {
        PooledObject pooledBullet = _interactor.bulletPool.GetPooledObject();
        pooledBullet.gameObject.SetActive(true);

        //Rigidbody bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody bullet = pooledBullet.GetComponent<Rigidbody>();
        bullet.transform.position = shootPoint.position;
        bullet.transform.rotation = shootPoint.rotation;

        bullet.velocity = shootPoint.forward * _interactor.GetShootVelocity();
        //Destroy(bullet.gameObject, 5f);

        _interactor.bulletPool.DestroyPooledObject(pooledBullet, 5.0f);
    }
}
