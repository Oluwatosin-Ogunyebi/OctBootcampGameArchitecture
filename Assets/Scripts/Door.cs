using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator doorAnim;

    private void Awake()
    {
        doorAnim = GetComponent<Animator>();    
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorAnim.SetBool("isDoorOpen", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorAnim.SetBool("isDoorOpen", false);
        }
    }
}
