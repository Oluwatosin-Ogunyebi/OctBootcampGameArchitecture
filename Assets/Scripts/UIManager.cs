using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Health playerHealth;

    [Header("UI Elements")]
    public TMP_Text healthTxt;
    public GameObject gameOverTxt;
    // Start is called before the first frame update
    void Start()
    {
        gameOverTxt.SetActive(false);
    }
    private void OnEnable()
    {
        playerHealth.OnHealthUpdated += OnHealthUpdate;
        playerHealth.OnDeath += OnDeath;
    }

    private void OnHealthUpdate(float health)
    {
        healthTxt.text = "Health: " + Mathf.Floor(health).ToString();
    }
    void OnDeath()
    {
        gameOverTxt.SetActive(true);
    }
    private void OnDestroy()
    {
        playerHealth.OnHealthUpdated -= OnHealthUpdate;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
