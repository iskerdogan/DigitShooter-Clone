using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int count;
    public OperationType operationType;

    private DigitController digitController;

    private void Awake()
    {
        digitController = GetComponent<DigitController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var throwMoney = other.GetComponent<ThrowMoney>();
        if (throwMoney != null)
        {
            switch (operationType)
            {
                case OperationType.Div:
                    break;
                case OperationType.Mul:
                    break;
                case OperationType.Sub:
                    count -= GameManager.Instance.ThrowMoneyCount;
                    if (count <= 0)
                    {
                        count = Mathf.Abs(count);
                        operationType = OperationType.Sum;
                    }
                    digitController.ChangeTheStructureOfMoney(count,operationType);
                    throwMoney.gameObject.SetActive(false);
                    break;
                case OperationType.Sum:
                    count += GameManager.Instance.ThrowMoneyCount;
                    digitController.ChangeTheStructureOfMoney(count,operationType);
                    throwMoney.gameObject.SetActive(false);
                    break;
            }
        }
    }
}