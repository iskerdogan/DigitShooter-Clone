using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;

    private void Start()
    {
        startPanel.SetActive(true);
    }

    public void HoldAndMoveClicked()
    {
        startPanel.SetActive(false);
        GameManager.Instance.ChangeGameState(GameState.InGame);
        
    }
}