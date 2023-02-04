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
            ChangeTheStructureOfMoney(collectable.count,collectable.operationType);
        }
        if (GetComponent<ThrowMoney>())
        {
            ChangeTheStructureOfMoney(GameManager.Instance.ThrowMoneyCount);
        }
    }


    public void ChangeTheStructureOfMoney(int currentMoney,OperationType operationType = OperationType.None)
    {
        switch (operationType)
        {
            case OperationType.Sub:
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
        dollarSymbol.GetComponent<MeshRenderer>().material.color = Color.red;
        for (int i = 0; i < Digit1List.Count; i++)
        {
            Digit1List[i].GetComponent<MeshRenderer>().material.color = Color.red;
            Digit10List[i].GetComponent<MeshRenderer>().material.color = Color.red;
            Digit100List[i].GetComponent<MeshRenderer>().material.color = Color.red;
            Digit1000List[i].GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }
    
    private void ChangeColorGreen()
    {
        dollarSymbol.GetComponent<MeshRenderer>().material.color = Color.green;
        for (int i = 0; i < Digit1List.Count; i++)
        {
            Digit1List[i].GetComponent<MeshRenderer>().material.color = Color.green;
            Digit10List[i].GetComponent<MeshRenderer>().material.color = Color.green;
            Digit100List[i].GetComponent<MeshRenderer>().material.color = Color.green;
            Digit1000List[i].GetComponent<MeshRenderer>().material.color = Color.green;
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

    
}
