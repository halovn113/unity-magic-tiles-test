using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Manager")]
    public TileManager tileManager;
    public UIManager uiManager;
    public EffectManager effectManager;
    public GameOver gameOver;
    public ObserverManager observerManager;
    public GameRunner gameRunner;

    private GameState gameState;

    void Awake()
    {
        observerManager = new ObserverManager();
        gameRunner = FindObjectOfType<GameRunner>();
        tileManager = FindObjectOfType<TileManager>();
        uiManager = FindObjectOfType<UIManager>();

        effectManager.OnAwake(observerManager);
        uiManager.OnAwake(observerManager);
        gameOver.OnAwake(observerManager, this);
        tileManager.OnAwake(observerManager.tileTouchingObserver, observerManager, gameRunner);
        gameRunner.OnAwake(tileManager, uiManager, observerManager, this);
    }

    void Start()
    {
        gameRunner.OnStart();
        ChangeState(GameState.Game);
    }

    void Update()
    {
        var smoothDeltaTime = Time.smoothDeltaTime;

        switch (gameState)
        {
            case GameState.Title:
                break;
            case GameState.Game:
                gameRunner.OnUpdate(smoothDeltaTime);
                tileManager.OnUpdate(smoothDeltaTime);
                break;
            case GameState.GameOver:
                break;
        }
    }

    public void ChangeState(GameState newState)
    {
        gameState = newState;
        switch (newState)
        {
            case GameState.Game:
                gameRunner.ResetGame();
                break;
            case GameState.GameOver:
                gameOver.ShowGameOver(gameRunner.scoreBehaviour.score, gameRunner.IsWin(), gameRunner.scoreBehaviour.GetTimingResultInfos());
                break;
        }
    }
}
