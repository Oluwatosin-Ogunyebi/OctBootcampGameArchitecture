using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftDoor : MonoBehaviour
{
    private Animator doorAnim;

    private float timer = 0;
    [SerializeField] private Renderer doorRenderer;
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color activeColor;


    [SerializeField] private float waitTime = 1.0f;
    [SerializeField] private Lift lift;

    private void Awake()
    {
        doorAnim = GetComponent<Animator>();
        if (doorRenderer != null)
        {
            defaultColor = doorRenderer.material.color;
        }
    }
    private void OnTriggerEnter(Collider other)
    {   
        if (lift.GetLiftMovement()) { return; }
        if (other.CompareTag("Player"))
        {
            timer = 0;
            doorRenderer.material.color = activeColor;
            //doorAnim.SetBool("isDoorOpen", true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (lift.GetLiftMovement()) { return; }

        if (!other.CompareTag("Player")) { return; } //Guard clause to exit if not player
        timer += Time.deltaTime;

        if (timer >= waitTime)
        {
            timer = waitTime;
            doorAnim.SetBool("isDoorOpen", true);
            waitTime *= 1.5f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (lift.GetLiftMovement()) { return; }
        if (other.CompareTag("Player"))
        {
            doorAnim.SetBool("isDoorOpen", false);
            doorRenderer.material.color = defaultColor;
        }
    }
}
