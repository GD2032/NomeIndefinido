using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] Transform player;
    Vector3 TargetPosition;
    Vector3 velocity = Vector3.zero;
    float smooth;
    void Start()
    {
        smooth = 0.2f;
    }
    void FixedUpdate()
    {
        TargetPosition = player.transform.position;
        TargetPosition.z = transform.position.z;
        transform.position = Vector3.SmoothDamp(transform.position,TargetPosition,ref velocity,smooth);
    }
}
