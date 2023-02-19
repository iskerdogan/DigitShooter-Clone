using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static event Action<GameState> OnStateChanged;
    public GameState State { get; private set; }

    
    private int playerStartMoney;
    private int throwMoneyCount = 2;
    private float throwMoneyFireRate = 1f; //1.5f
    private float throwMoneyRange = 25; //15
    private int moneyEarnedInLevel;
    
    public int PlayerStartMoney
    {
        get { return playerStartMoney; }
        set
        {
            playerStartMoney = value;
        }
    }

    public int ThrowMoneyCount
    {
        get { return throwMoneyCount; }
        set
        {
            throwMoneyCount = value;
        }
    }

    public float ThrowMoneyFireRate
    {
        get { return throwMoneyFireRate; }
        set
        {
            throwMoneyFireRate = value;
        }
    }

    public float ThrowMoneyRange
    {
        get { return throwMoneyRange; }
        set
        {
            throwMoneyRange = value;
        }
    }
    
    public int MoneyEarnedInLevel
    {
        get { return moneyEarnedInLevel; }
        set
        {
            moneyEarnedInLevel = value;
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start() => ChangeGameState(GameState.Start);

    public void ChangeGameState(GameState newState)
    {
        State = newState;
        switch (State)
        {
            case GameState.Start:
                moneyEarnedInLevel = 0;
                break;
            case GameState.InGame:
                break;
            case GameState.Success:
                break;
            case GameState.Fail:
                break;
        }
        OnStateChanged?.Invoke(State);
    }
}
