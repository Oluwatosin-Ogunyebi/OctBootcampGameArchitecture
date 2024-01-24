using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactor : MonoBehaviour
{
    protected PlayerInput playerInput;

    private void Start()
    {
        playerInput = PlayerInput.GetInstance();
    }
    void Update()
    {
        Interact(); 
    }

    public abstract void Interact();
}
