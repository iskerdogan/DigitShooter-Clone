using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int playerStartMoney;
    private int throwMoneyCount = 2;
    private float throwMoneyFireRate = .1f;
    private float throwMoneyRange = 30;

    public int PlayerStartMoney => playerStartMoney;
    public int ThrowMoneyCount => throwMoneyCount;
    public float ThrowMoneyFireRate => throwMoneyFireRate;
    public float ThrowMoneyRange => throwMoneyRange;

    private void Awake()
    {
        Instance = this;
    }
}
