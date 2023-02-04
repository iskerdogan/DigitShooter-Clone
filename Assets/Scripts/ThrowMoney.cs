using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowMoney : MonoBehaviour
{
    private DigitController digitController;
    private int count;

    private void Awake()
    {
        digitController = GetComponent<DigitController>();
        count = GameManager.Instance.ThrowMoneyCount;
    }

    private void Start()
    {
        digitController.ChangeTheStructureOfMoney(count);
    }
}
