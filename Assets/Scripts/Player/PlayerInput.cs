using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class PlayerInput : MonoBehaviour
{
    public float horizontal { get; private set; }
    public float vertical { get; private set; }
    public float mouseX { get; private set; }
    public float mouseY { get; private set; }

    public bool sprintHeld { get; private set; }

    public bool jumPressed { get; private set; }
    public bool primaryButtonPressed { get; private set; } //Left Click
    public bool secondaryButtonPressed { get; private set; } //Right Click
    public bool activatePressed { get; private set; }

    public bool weapon1Pressed { get; private set; }
    public bool weapon2Pressed { get; private set; }
    public bool commandPressed { get; private set; }

    public bool clear;

    //Singleton
    private static PlayerInput instance;

    public static PlayerInput GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        ClearInputs();
        ProcessInputs();
    }

    void ProcessInputs()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        sprintHeld = sprintHeld || Input.GetButton("Sprint");
        jumPressed = jumPressed || Input.GetButtonDown("Jump");

        activatePressed = activatePressed || Input.GetKeyDown(KeyCode.E);
        primaryButtonPressed = primaryButtonPressed || Input.GetButtonDown("Fire1");
        secondaryButtonPressed = secondaryButtonPressed || Input.GetButtonDown("Fire2");

        weapon1Pressed = weapon1Pressed || Input.GetKeyDown(KeyCode.Alpha1);
        weapon2Pressed = weapon2Pressed || Input.GetKeyDown(KeyCode.Alpha2);
        commandPressed = commandPressed || Input.GetKeyDown(KeyCode.G);

    }
    private void FixedUpdate()
    {
        clear = true;
    }

    void ClearInputs()
    {
        if (!clear) return;

        horizontal = 0;
        vertical = 0;
        mouseX = 0;
        mouseY = 0;

        sprintHeld = false;
        jumPressed = false;

        activatePressed = false;
        primaryButtonPressed = false;
        secondaryButtonPressed = false;

        weapon1Pressed = false;
        weapon2Pressed = false;

        commandPressed = false;


    }
}
