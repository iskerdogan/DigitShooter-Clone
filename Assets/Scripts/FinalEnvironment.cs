using TMPro;
using UnityEngine;

public class FinalEnvironment : MonoBehaviour
{
    public int count;
    public TextMeshPro countText;
    public FinalMoney finalMoney;
    private Rigidbody finalMoneyRb;

    private void Start()
    {
        countText.text = count.ToString();
        finalMoneyRb = finalMoney.GetComponent<Rigidbody>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        var throwMoney = other.GetComponent<ThrowMoney>();
        if (throwMoney != null)
        {
            throwMoney.gameObject.SetActive(false);
            count -= GameManager.Instance.ThrowMoneyCount;
            countText.text =  count.ToString();
            if (count <= 0)
            {
                finalMoneyRb.isKinematic = false;
                this.gameObject.SetActive(false);
            }
        }
    }
}