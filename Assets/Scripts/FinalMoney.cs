using System;
using UnityEngine;

public class FinalMoney : MonoBehaviour
{
    public GameObject moneyFx;
    public int earning;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            moneyFx.SetActive(true);
            GameManager.Instance.MoneyEarnedInLevel += earning;
            this.gameObject.SetActive(false);
        }
    }
}