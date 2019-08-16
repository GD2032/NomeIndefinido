using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour_1 : MonoBehaviour
{
    [SerializeField] Rigidbody2D rbPlayer;
    [SerializeField] private float speed;
    private float velocity_x;
    
    void Start()
    {
        speed = 4;
    }
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        Moviment();
    }
    void Moviment(){
        velocity_x = Input.GetAxisRaw("Horizontal");   
        rbPlayer.velocity = new Vector2(velocity_x * speed,rbPlayer.velocity.y);
    }
}
