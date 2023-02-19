using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity.VisualScripting;
using UnityEngine;

public class DigitController : MonoBehaviour
{
    [SerializeField] private List<GameObject> Digit1List;
    [SerializeField] private List<GameObject> Digit10List;
    [SerializeField] private List<GameObject> Digit100List;
    [SerializeField] private List<GameObject> Digit1000List;
    [SerializeField] private GameObject dollarSymbol;
    [SerializeField] private GameObject subSymbol;
    [SerializeField] private Material green;
    [SerializeField] private Material red;
    [SerializeField] private Material gray;

    private int digit1 = -1;
    private int digit10 = -1;
    private int digit100 = -1;
    private int digit1000 = -1;

    private Collectable collectable;

    private void Awake()
    {
        collectable = GetComponent<Collectable>();
    }

    private void Start()
    {
        if (collectable)
        {
            ChangeTheStructureOfMoney(collectable.count, collectable.operationType);
        }

        if (GetComponent<ThrowMoney>())
        {
            ChangeTheStructureOfMoney(GameManager.Instance.ThrowMoneyCount);
        }
    }


    public void ChangeTheStructureOfMoney(int currentMoney, OperationType operationType = OperationType.None)
    {
        switch (operationType)
        {
            case OperationType.Sub:
                if (CountNumberOfDigits(currentMoney) == 1) subSymbol.transform.localPosition = Vector3.zero;
                ChangeColorRed();
                break;
            case OperationType.Sum:
                ChangeColorGreen();
                break;
        }

        CloseAll();
        FindTheDigits(currentMoney);
        OpenDigitInList();
    }

    private void FindTheDigits(int number)
    {
        digit1 = number % 10;

        var temp = number / 10;
        if (temp < 1) return;
        digit10 = temp % 10;

        temp = number / 100;
        if (temp < 1) return;
        digit100 = temp % 10;

        temp = number / 1000;
        if (temp < 1) return;
        digit1000 = temp % 10;
    }


    private void OpenDigitInList()
    {
        Digit1List[digit1].SetActive(true);
        digit1 = -1;

        if (digit10 == -1) return;
        Digit10List[digit10].SetActive(true);
        digit10 = -1;

        if (digit100 == -1) return;
        Digit100List[digit100].SetActive(true);
        digit100 = -1;

        if (digit1000 == -1) return;
        Digit1000List[digit1000].SetActive(true);
        digit1000 = -1;
    }

    private void ChangeColorRed()
    {
        subSymbol.GetComponent<MeshRenderer>().material = red;
        subSymbol.SetActive(true);
        dollarSymbol.GetComponent<MeshRenderer>().material = red;
        for (int i = 0; i < Digit1List.Count; i++)
        {
            Digit1List[i].GetComponent<MeshRenderer>().material = red;
            Digit10List[i].GetComponent<MeshRenderer>().material = red;
            Digit100List[i].GetComponent<MeshRenderer>().material = red;
            Digit1000List[i].GetComponent<MeshRenderer>().material = red;
        }
    }

    private void ChangeColorGreen()
    {
        subSymbol.GetComponent<MeshRenderer>().material = green;
        subSymbol.SetActive(false);
        dollarSymbol.GetComponent<MeshRenderer>().material = green;
        for (int i = 0; i < Digit1List.Count; i++)
        {
            Digit1List[i].GetComponent<MeshRenderer>().material = green;
            Digit10List[i].GetComponent<MeshRenderer>().material = green;
            Digit100List[i].GetComponent<MeshRenderer>().material = green;
            Digit1000List[i].GetComponent<MeshRenderer>().material = green;
        }
    }
    
    public void ChangeColorGray()
    {
        dollarSymbol.GetComponent<MeshRenderer>().material = gray;
        for (int i = 0; i < Digit1List.Count; i++)
        {
            Digit1List[i].GetComponent<MeshRenderer>().material = gray;
            Digit10List[i].GetComponent<MeshRenderer>().material = gray;
            Digit100List[i].GetComponent<MeshRenderer>().material = gray;
            Digit1000List[i].GetComponent<MeshRenderer>().material = gray;
        }
    }

    private void CloseAll()
    {
        for (int i = 0; i < Digit1List.Count; i++)
        {
            Digit1List[i].SetActive(false);
            Digit10List[i].SetActive(false);
            Digit100List[i].SetActive(false);
            Digit1000List[i].SetActive(false);
        }
    }

    public int CountNumberOfDigits(int money)
    {
        var count = 0;
        while (money > 0)
        {
            money /= 10;
            count++;
        }

        return count;
    }
}