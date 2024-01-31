using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShootStrategy : IShootStrategy
{
    ShootInteractor interactor;
    Transform shootPoint;
    public BulletShootStrategy(ShootInteractor interactor)
    {
        Debug.Log("Switched to Bullet");
        this.interactor = interactor;
        shootPoint = interactor.GetShootPoint();


    }
    public void Shoot()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
