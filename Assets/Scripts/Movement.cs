using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private DynamicJoystick dynamicJoystick;
    [SerializeField] private float limitX;
    [SerializeField] private float speed;
    [SerializeField] private float speedX;

    void Update()
    {
          var newX = Mathf.Clamp
                (
                    transform.position.x + speedX * dynamicJoystick.Horizontal * Time.deltaTime,
                    -limitX,
                    limitX
                );
                transform.position = new Vector3(newX, transform.position.y, transform.position.z + speed * Time.deltaTime);
    }
}
