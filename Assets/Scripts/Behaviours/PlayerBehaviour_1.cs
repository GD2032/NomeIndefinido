﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour_1 : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rbPlayer;
    [SerializeField] private GameObject InterfaceCanvas;
    [SerializeField] private float speed, forceJump, radius, groundTestP,wallTestP,wallTestD,time;
    private float velocity_x;
    private Collider2D[] olReturn;
    private bool inGround, frontRight;
    private RaycastHit2D wallTest;
    private UI ui;
   
    void Start()
    {
        ui = InterfaceCanvas.GetComponent<UI>();
        speed = 6;
        forceJump = 12;
        radius = 0.2f;
        groundTestP = 0.2f;
        ui.Fades(true,1,2);
    }
     void Update()
    {
        
        inGround = GroundTest();
        Jump();
    }
    void FixedUpdate()
    {
        Moviment();
    }
    void Moviment(){
        velocity_x = Input.GetAxisRaw("Horizontal");   
        rbPlayer.velocity = new Vector2(velocity_x * speed,rbPlayer.velocity.y);              
    }
    void Jump()
    {
        if(Input.GetButton("Jump") && inGround)
        {
            rbPlayer.velocity = Vector2.up * forceJump;
            inGround = false;
        }
        print(rbPlayer.velocity.y);
        if (Input.GetButtonUp("Jump") && rbPlayer.velocity.y >= 0)
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x,0);
    }
    bool GroundTest()
    {
        wallTest = Physics2D.Raycast(new Vector2(transform.position.x + wallTestP, transform.position.y), frontRight ? Vector2.right : Vector2.left, wallTestD);

        olReturn = Physics2D.OverlapCircleAll(new Vector2(transform.position.x,transform.position.y - groundTestP), radius);
        foreach (Collider2D col in olReturn)
        {
            if (col.tag == "Ground")
                return true;
        }
        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y - groundTestP), radius);
    }
}
