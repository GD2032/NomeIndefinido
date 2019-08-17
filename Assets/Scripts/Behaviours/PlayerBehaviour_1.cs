using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour_1 : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rbPlayer;
    [SerializeField] private float speed, forceJump, radius, groundTestP,wallTestP,wallTestD;
    private float velocity_x;
    private Collider2D[] olReturn;
    private bool inGround, frontRight;
    private RaycastHit2D wallTest;
    void Start()
    { 
        speed = 6;
        forceJump = 6;
        radius = 0.3f;
        groundTestP = 0.65f;
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
        if(Input.GetButtonDown("Fire1") && inGround)
        {
            rbPlayer.velocity = Vector2.up * forceJump;
            inGround = false;
        }
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
