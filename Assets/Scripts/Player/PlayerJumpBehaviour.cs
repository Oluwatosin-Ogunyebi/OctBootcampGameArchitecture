using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovementBehaviour))]
public class PlayerJumpBehaviour : Interactor
{

    [Header("Player Jump")]
    [SerializeField] private float jumpVelocity;

    private PlayerMovementBehaviour playerMovementBehaviour;


    public override void Interact()
    {
        if (playerMovementBehaviour == null)
        {
            playerMovementBehaviour = GetComponent<PlayerMovementBehaviour>();
        }

        if (playerInput.jumPressed && playerMovementBehaviour.isGrounded)
        {
            playerMovementBehaviour.SetYVelocity(jumpVelocity);
        }

    }
}
