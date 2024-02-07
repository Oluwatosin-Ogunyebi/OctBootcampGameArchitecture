using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelManager[] levels;
    private static GameManager Instance;

    private GameState currentState;
    private bool isInputActive = true;

    private LevelManager currentLevel;
    private int currentLevelIndex = 0;


    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public static GameManager GetInstance()
    {
        return Instance;
    }

    public bool IsInputActive()
    {
        return isInputActive;
    }
    // Start is called before the first frame update
    void Start()
    {
        //Go to the briefing state of the game
        if (levels.Length > 0)
        {
            Changestate(GameState.Briefing, levels[currentLevelIndex]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Changestate(GameState state, LevelManager level)
    {
        currentState = state;
        currentLevel = level;

        switch (currentState)
        {
            case GameState.Briefing:
                StartBriefing();
                break;
            case GameState.LevelStart:
                InitLevel();
                break;
            case GameState.LevelIn:
                RunLevel();
                break;
            case GameState.LevelEnd:
                CompleteLevel();
                break;
            case GameState.GameOver:
                GameOver();
                break;
            case GameState.GameEnd:
                GameEnd();
                break;
        }

    }
    private void StartBriefing()
    {
        Debug.Log("Well, briefing Started");

        //Disable Player Input
        isInputActive = false;

        //Start the level
        Changestate(GameState.LevelStart, currentLevel);
    }

    private void InitLevel()
    {
        Debug.Log("Well, Level Initialised");
        isInputActive = true;

        currentLevel.StartLevel();
        Changestate(GameState.LevelIn, currentLevel);
    }

    private void RunLevel()
    {
        Debug.Log("Well, Level Running");
    }

    private void CompleteLevel()
    {
        Debug.Log("Well, Level Completed");

        //Go to the next level
        Changestate(GameState.LevelStart, levels[++currentLevelIndex]);
    }

    private void GameOver()
    {
        Debug.Log("Well, Level Game Over and You Lose");
    }

    private void GameEnd()
    {
        Debug.Log("Well, Game has Ended and You Win");
    }
    public enum GameState
    {
        Briefing,
        LevelStart,
        LevelIn,
        LevelEnd,
        GameOver,
        GameEnd
    }
}
