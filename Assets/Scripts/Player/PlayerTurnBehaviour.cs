using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerTurnBehaviour : Interactor
{
    [Header("Player Turn")]
    [SerializeField] private float turnSpeed = 10.0f;

    public override void Interact()
    {
        //Player turn movement
        transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime * playerInput.mouseX);
    }
}
