using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;
    void Awake() {
        Instance = this;
    }

    void Start()
    {
     
    }

    // Update is called once per frame
    public void UpdateGameState(GameState newstate){
        State = newstate;

        switch (newstate){
            case GameState.Menu:
                break;
            case GameState.Lose:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newstate), newstate,null);
        }

        OnGameStateChanged?.Invoke(newstate);
    }

    public enum GameState{
        Menu,
        Lose
    }
}
