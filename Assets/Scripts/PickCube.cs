using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickCube : MonoBehaviour, IPickable
{
    FixedJoint joint;
    Rigidbody cubeRb;

    private void Start()
    {
        cubeRb = GetComponent<Rigidbody>();
    }
    public void OnDropped()
    {
        cubeRb.isKinematic = false;
        cubeRb.useGravity = true;
        transform.SetParent(null);
    }

    public void OnPicked(Transform attachTransform)
    {
        //Pickup item
        transform.position = attachTransform.position;
        transform.rotation = attachTransform.rotation;
        transform.SetParent(attachTransform);

        cubeRb.isKinematic = true;
        cubeRb.useGravity = false;
    }


}
