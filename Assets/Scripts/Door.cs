using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int count;
    public OperationType operationType;
    public DoorType doorType;
    public TextMeshPro doorNumberText;
    public TextMeshPro doorTypeText;
    public Material green;
    public Material red;

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        switch (operationType)
        {
            case OperationType.Sub:
                meshRenderer.material = red;
                doorNumberText.text = "-" + count;
                break;
            case OperationType.Sum:
                meshRenderer.material = green;
                doorNumberText.text = "+" + count;
                break;
        }
        
        switch (doorType)
        {
            case DoorType.Range:
                doorTypeText.text = "RANGE";
                break;
            case DoorType.FireRate:
                doorTypeText.text = "FIRERATE";
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var throwMoney = other.GetComponent<ThrowMoney>();
        if (throwMoney != null)
        {
            switch (operationType)
            {
                case OperationType.Sub:
                    count -= GameManager.Instance.ThrowMoneyCount;
                    doorNumberText.text = "-" + count;
                    if (count <= 0)
                    {
                        count = Mathf.Abs(count);
                        doorNumberText.text = "+" + count;
                        meshRenderer.material = green;
                        operationType = OperationType.Sum;
                    }
                    throwMoney.gameObject.SetActive(false);
                    break;
                case OperationType.Sum:
                    count += GameManager.Instance.ThrowMoneyCount;
                    doorNumberText.text = "+" + count;
                    throwMoney.gameObject.SetActive(false);
                    break;
            }
        }
    }
}