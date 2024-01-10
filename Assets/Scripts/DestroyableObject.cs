using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour, IDestroyable
{
    public void OnCollided()
    {
        Destroy(gameObject, 2.0f);
    }
}
