using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject throwMoneyPrefab;
    [SerializeField] private Transform throwMoneyPos;
    [SerializeField] private Transform throwMoneyParent;
    [SerializeField] private GameObject digitParent;
    [SerializeField] private List<Transform> digitTransforms;
    public List<GameObject> pooledThrowMoney;
    public int amountToPool;
    
    private DigitController digitController;
    private int moneyCount = 0;

    private void Awake()
    {
        digitController = GetComponent<DigitController>();
    }

    private void Start()
    {
        GameManager.OnStateChanged += OnStateChanged;
        
        CreateThrowMoney();
        digitController.ChangeTheStructureOfMoney(moneyCount);
        digitParent.transform.position = digitTransforms[0].position;
    }

    private void OnTriggerEnter(Collider other)
    {
        var collectable = other.GetComponent<Collectable>();
        if (collectable != null)
        {
            switch (collectable.operationType)
            {
                case OperationType.Sub:
                    moneyCount -= collectable.count;
                    break;
                case OperationType.Sum:
                    moneyCount += collectable.count;
                    break;
            }

            var numberOfDigitCount = digitController.CountNumberOfDigits(moneyCount);
            digitParent.transform.position = digitTransforms[numberOfDigitCount -1].position;
            digitController.ChangeTheStructureOfMoney(moneyCount);
            collectable.gameObject.SetActive(false);
        }

        var door = other.GetComponent<Door>();
        if (door != null)
        {
            switch (door.doorType)
            {
                case DoorType.Range:
                    switch (door.operationType)
                    {
                        case OperationType.Sum:
                            GameManager.Instance.ThrowMoneyRange += (float)door.count/2;
                            break;
                        case OperationType.Sub:
                            GameManager.Instance.ThrowMoneyRange -= (float)door.count/2;
                            break;
                    }
                    break;
                case DoorType.FireRate:
                    switch (door.operationType)
                    {
                        case OperationType.Sum:
                            GameManager.Instance.ThrowMoneyFireRate -= (float)door.count/30;
                            break;
                        case OperationType.Sub:
                            GameManager.Instance.ThrowMoneyFireRate += (float)door.count/30;
                            break;
                    }
                    break;
            }
            door.gameObject.SetActive(false);
        }
    }

    private IEnumerator ThrowMoney()
    {
        while (GameManager.Instance.State == GameState.InGame)
        {
            var throwMoney = GetPooledThrowMoney();
            throwMoney.transform.position = throwMoneyPos.position;
            throwMoney.SetActive(true);
            throwMoney.transform.DOMoveZ(throwMoney.transform.position.z + GameManager.Instance.ThrowMoneyRange,2f).SetEase(Ease.Linear).OnComplete(() =>
            {
                throwMoney.SetActive(false);
            });
            yield return new WaitForSeconds(GameManager.Instance.ThrowMoneyFireRate);
        }
    }

    

    private void CreateThrowMoney()
    {
        pooledThrowMoney = new List<GameObject>();
        for(int i = 0; i < amountToPool; i++)
        {
            var tmp = Instantiate(throwMoneyPrefab, throwMoneyPos.position, Quaternion.identity,throwMoneyParent);
            tmp.SetActive(false);
            pooledThrowMoney.Add(tmp);
        }
    }

    private int poolCount;
    
    private int IncreasePoolCount()
    {
        return poolCount++;
    }
    
    private GameObject GetPooledThrowMoney()
    {
        IncreasePoolCount();
        if (poolCount<amountToPool-1)
        {
            return pooledThrowMoney[poolCount];
        }

        poolCount = 0;
        return pooledThrowMoney[poolCount];
    }
    
    private void OnStateChanged(GameState State)
    {
        switch (State)
        {
            case GameState.Start:
                break;
            case GameState.InGame:
                StartCoroutine(ThrowMoney());
                break;
            case GameState.Success:
                break;
            case GameState.Fail:
                break;
        }
    }
}