using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Manager")]
    public TileManager tileManager;
    public UIManager uiManager;

    public ObserverManager observerManager;
    public GameRunner gameRunner;

    void Awake()
    {
        observerManager = new ObserverManager();
        gameRunner = FindObjectOfType<GameRunner>();
        tileManager = FindObjectOfType<TileManager>();
        uiManager = FindObjectOfType<UIManager>();

        uiManager.OnAwake(observerManager);
        tileManager.OnAwake(observerManager.tileTouchingObserver, observerManager);
        gameRunner.OnAwake(tileManager, uiManager, observerManager);
    }

    void Start()
    {
        gameRunner.OnStart();
    }

    void Update()
    {
        var smoothDeltaTime = Time.smoothDeltaTime;
        gameRunner.OnUpdate(smoothDeltaTime);
        tileManager.OnUpdate(smoothDeltaTime);
    }
}
