using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject objectToPool;
    public int startSize;


    [SerializeField] private List<PooledObject> activePool = new List<PooledObject>();
    [SerializeField] private List<PooledObject> usedPool = new List<PooledObject>();

    private PooledObject tempObject;
    // Start is called before the first frame update
    void Start()
    {
        InitialisePool();
    }

    public void InitialisePool()
    {
        for (int i = 0; i < startSize; i++)
        {
            AddNewObject();
        }
    }

    public void AddNewObject()
    {
        tempObject = Instantiate(objectToPool, transform).GetComponent<PooledObject>();
        tempObject.gameObject.SetActive(false);
        tempObject.SetObjectPool(this);
        activePool.Add(tempObject);
    }

    public PooledObject GetPooledObject()
    {
        PooledObject tempObjectToPool;

        if (activePool.Count > 0)
        {
            tempObjectToPool = activePool[0];
            usedPool.Add(tempObjectToPool);
            activePool.RemoveAt(0);
        }
        else
        {
            AddNewObject();
            tempObjectToPool = GetPooledObject();
        }
        return tempObjectToPool;
    }

    public void DestroyObjectFromPool(PooledObject obj, float time = 0)
    {
        if (time == 0)
        {
            obj.Destroy();
        }
        else
        {
            obj.Destroy(time);
        }
    }

    public void RestoreObject(PooledObject obj)
    {
        Debug.Log("Object has been restored");
        obj.gameObject.SetActive(false);
        usedPool.Remove(obj);
        activePool.Add(obj);
    }
}
