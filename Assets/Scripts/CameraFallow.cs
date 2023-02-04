using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFallow : MonoBehaviour
{
    [SerializeField] 
    private Movement player;

    [SerializeField] 
    private Vector3 offset; 

    private void LateUpdate()
    {
        var vector = new Vector3(0,player.transform.position.y ,player.transform.position.z) + offset;
        transform.position = Vector3.Lerp(transform.position, vector,1.5f);
    }
}
