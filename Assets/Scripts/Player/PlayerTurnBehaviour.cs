using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerTurnBehaviour : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [Header("Player Turn")]
    [SerializeField] private float turnSpeed = 10.0f;

    void Update()
    {
        RotatePlayer();
    }

    private void RotatePlayer()
    {
        //Player turn movement
        transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime * playerInput.mouseX);
    }
}
