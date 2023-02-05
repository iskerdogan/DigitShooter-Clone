using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private DynamicJoystick dynamicJoystick;
    [SerializeField] private float limitX;
    [SerializeField] private float speed;
    [SerializeField] private float speedX;

    private float currenSpeed;
    private float currenSpeedX;

    private void Start()
    {
        GameManager.OnStateChanged += OnStateChanged;
    }
    
    void Update()
    {
          var newX = Mathf.Clamp
                (
                    transform.position.x + currenSpeedX * dynamicJoystick.Horizontal * Time.deltaTime,
                    -limitX,
                    limitX
                );
                transform.position = new Vector3(newX, transform.position.y, transform.position.z + currenSpeed * Time.deltaTime);
    }
    
    private void OnStateChanged(GameState State)
    {
        switch (State)
        {
            case GameState.Start:
                currenSpeed = 0;
                currenSpeedX = 0;
                break;
            case GameState.InGame:
                currenSpeed = speed;
                currenSpeedX = speedX;
                break;
            case GameState.Success:
                break;
            case GameState.Fail:
                break;
        }
    }

}