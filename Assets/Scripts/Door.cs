using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int count;
    public OperationType operationType;
    public DoorType doorType;
    public Door sidedoor;
    public TextMeshPro doorNumberText;
    public TextMeshPro doorTypeText;
    public Material green;
    public Material red;

    private MeshRenderer meshRenderer;
    private BoxCollider sideDoorCollider;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        sideDoorCollider = sidedoor.GetComponent<BoxCollider>();
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
            transform.DOScale(new Vector3(1.19f, 0.66f, 1.10f), .1f).OnComplete(() =>
            {
                transform.DOScale(new Vector3(1.07f, 0.6f, 1), .1f);
            });
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
        
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            sideDoorCollider.enabled = false;
            gameObject.SetActive(false);
        }

    }
}