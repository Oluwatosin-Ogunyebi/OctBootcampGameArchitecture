using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PooledObject : MonoBehaviour
{
    ObjectPool associatedPool;

    public UnityEvent OnReset;


    private float timer = 0;
    private float destroyTIme = 0;
    private bool setToDestroy = false;
    public void SetObjectPool(ObjectPool pool)
    {
        associatedPool = pool;
        timer = 0;
        destroyTIme = 0;
        setToDestroy = false;
    }

    void Update()
    {
        if (setToDestroy)
        {
            timer += Time.deltaTime;
            if (timer >= destroyTIme)
            {
                setToDestroy = false;
                timer = 0;

                Destroy();
            }
        } 
    }

    public void ResetObject()
    {
        OnReset?.Invoke();
    }

    public void Destroy()
    {
        if (associatedPool != null)
        {
            associatedPool.RestoreObject(this);
        }
    }

    public void Destroy(float time)
    {
        setToDestroy = true;
        destroyTIme = time;
    }
}
