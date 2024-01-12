using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushButton : MonoBehaviour, ISelectable
{
    [SerializeField] private Renderer buttonRenderer;
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color hoverColor;

    public UnityEvent OnPushButton;
    public void OnHoverEnter()
    {
        buttonRenderer.material.color = hoverColor;
    }

    public void OnHoverExit()
    {
        buttonRenderer.material.color = defaultColor;
    }

    public void OnSelect()
    {
        OnPushButton?.Invoke();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (buttonRenderer != null)
        {
            defaultColor = buttonRenderer.material.color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
