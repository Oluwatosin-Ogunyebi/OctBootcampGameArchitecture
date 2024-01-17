using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private float turnSpeed = 10.0f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private bool invertMouse;
    [SerializeField] private float  sprintMultiplier =2;


    [Header("Player Jump")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpVelocity;

    [Header("Player Shoot")]
    [SerializeField] private Rigidbody bulletPrefab;
    [SerializeField] private Rigidbody rocketPrefab;
    [SerializeField] private float shootForce;
    [SerializeField] private Transform shootPoint;

    [Header("Select Interaction")]
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask interactionLayerMask;
    [SerializeField] private float interactionDistance;

    [Header("Pickup Interaction")]
    [SerializeField] private LayerMask pickupLayerMask;
    [SerializeField] private float pickupDistance;
    [SerializeField] private Transform attachTransform;

    private CharacterController characterController;
    private float horizontalInput, verticalInput;
    private float mouseX, mouseY;
    private float moveMultiplier = 1.0f;
    private float camXRotation;
    private bool isGrounded;
    private Vector3 playerVelocity;

    //Interaction Raycasts
    private RaycastHit hit;
    private ISelectable selection;
    private IPickable pickable;

    private bool isPicked = false;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //Hide Mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        RotatePlayer();

        GroundCheck();
        MovePlayer();
        JumpCheck();

        Shoot();
        Interact();
        PickAndDrop();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        moveMultiplier = Input.GetButton("Sprint") ? sprintMultiplier : 1.0f;

    }

    private void MovePlayer()
    {
        characterController.Move((transform.forward * verticalInput + transform.right * horizontalInput)
            * moveSpeed * moveMultiplier * Time.deltaTime);

        //Ground Check
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        playerVelocity.y += gravity * Time.deltaTime;

        characterController.Move(playerVelocity * Time.deltaTime);
    }

    private void RotatePlayer()
    {
        //Player turn movement
        transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime * mouseX);

        //Camera upo/down movement
        camXRotation += Time.deltaTime * mouseY * turnSpeed * (invertMouse ? 1 : -1);
        camXRotation = Mathf.Clamp(camXRotation, -50.0f, 50.0f);

        cameraTransform.localRotation = Quaternion.Euler(camXRotation, 0, 0);
    }

    private void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);
    }

    private void JumpCheck()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerVelocity.y = jumpVelocity;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;

        Gizmos.DrawSphere(groundCheck.position, groundCheckDistance);
    }

    private void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Rigidbody bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            bullet.AddForce(shootPoint.forward * shootForce, ForceMode.Impulse);
            Destroy(bullet.gameObject, 5.0f);
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Rigidbody bullet = Instantiate(rocketPrefab, shootPoint.position, shootPoint.rotation);
            bullet.AddForce(shootPoint.forward * shootForce, ForceMode.Impulse);
            Destroy(bullet.gameObject, 5.0f);
        }

    }

    private void Interact()
    {
        //Cast a ray from middle of camera
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2));
        if (Physics.Raycast(ray, out hit, interactionDistance, interactionLayerMask))
        {
            Debug.Log("We hit " + hit.collider.name);
            Debug.DrawRay(ray.origin, ray.direction * interactionDistance, Color.red);

            selection = hit.transform.GetComponent<ISelectable>();
            if (selection != null)
            {
                selection.OnHoverEnter();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    selection.OnSelect();
                }
            }
        }

        if (hit.transform == null && selection != null)
        {
            selection.OnHoverExit();
            selection = null;
        }
    }


    private void PickAndDrop()
    {
        //Cast a ray
        if (Physics.Raycast(GetCamRay(), out hit,pickupDistance, pickupLayerMask))
        {
            if (Input.GetKeyDown(KeyCode.E)  && !isPicked)
            {
                pickable = hit.transform.GetComponent<IPickable>();
                if (pickable == null) return;

                pickable.OnPicked(attachTransform);
                isPicked = true;
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && isPicked && pickable != null)
        {
            pickable.OnDropped();
            isPicked = false;
        }
    }

    private Ray GetCamRay()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        return ray;
    }
}
