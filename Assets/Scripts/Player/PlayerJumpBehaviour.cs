using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovementBehaviour))]
public class PlayerJumpBehaviour : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;

    [Header("Player Jump")]
    [SerializeField] private float jumpVelocity;

    private PlayerMovementBehaviour playerMovementBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        playerMovementBehaviour = GetComponent<PlayerMovementBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        JumpCheck();
    }

    private void JumpCheck()
    {
        if (playerInput.jumPressed && playerMovementBehaviour.isGrounded)
        {
            playerMovementBehaviour.SetYVelocity(jumpVelocity);
        }

    }
}
