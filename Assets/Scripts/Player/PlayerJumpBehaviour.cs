using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovementBehaviour))]
public class PlayerJumpBehaviour : Interactor
{

    [Header("Player Jump")]
    [SerializeField] private float jumpVelocity;

    private PlayerMovementBehaviour playerMovementBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        playerMovementBehaviour = GetComponent<PlayerMovementBehaviour>();
    }

    public override void Interact()
    {
        if (playerInput.jumPressed && playerMovementBehaviour.isGrounded)
        {
            playerMovementBehaviour.SetYVelocity(jumpVelocity);
        }

    }
}
