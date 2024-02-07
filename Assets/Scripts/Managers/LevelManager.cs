using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private bool isFinalLevel = false;
    public UnityEvent onLevelStart;
    public UnityEvent onLevelEnd;
    

    public void StartLevel()
    {
        onLevelStart?.Invoke();
    }

    public void EndLevel()
    {
        onLevelEnd?.Invoke();

        if(isFinalLevel)
        {
            //TO DO: Let game manager know to change game state to Game End
        }
        else
        {
            //TO DO: Change state to Level End
        }
    }
}
