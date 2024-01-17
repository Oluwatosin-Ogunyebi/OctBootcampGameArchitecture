using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator doorAnim;

    private float timer = 0;
    [SerializeField] private Renderer doorRenderer;
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color activeColor;


    [SerializeField] private float waitTime = 1.0f;

    private bool isLocked = true;

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
        if (!isLocked && other.CompareTag("Player"))
        {   
            timer = 0;
            doorRenderer.material.color = activeColor;
            //doorAnim.SetBool("isDoorOpen", true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isLocked) { return; }
        if (!other.CompareTag("Player")) { return; } //Guard clause to exit if not player
        timer += Time.deltaTime;

        if (timer >= waitTime)
        {
            timer = waitTime;
            doorAnim.SetBool("isDoorOpen", true);
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorAnim.SetBool("isDoorOpen", false);
            doorRenderer.material.color = defaultColor;
        }
    }

    public void LockDoor()
    {
        isLocked = true;
    }
    
    public void UnlockDoor()
    {
        isLocked = false;
    }
}
