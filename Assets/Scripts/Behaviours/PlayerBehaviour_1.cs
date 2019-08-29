using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBehaviour_1 : MonoBehaviour
{
    
    [SerializeField] private Rigidbody2D rbPlayer;
    [SerializeField] private GameObject InterfaceCanvas, reflectShield, clone;
    [SerializeField] private AudioClip[] sound;
    [SerializeField] private float speed,playerScaleX,playerScaleY, forceJump,forceWallJump, radius, groundTestP; //P = position; D = distance
    [SerializeField] private float mana, manaCount;
    [SerializeField] private Vector2 jumpForce;
    private float velocity_x;
    private AudioSource outPutSound;
    private Collider2D[] olReturn;
    private bool inGround, frontRight, execute, moviment;
    private RaycastHit2D wallTest;
    private UI ui;
   
    void Start()
    {
        moviment = true;
        outPutSound = GetComponent<AudioSource>();
        outPutSound.volume = 0.008f;
        ui = InterfaceCanvas.GetComponent<UI>();
        ui.Fades(true,1,Random.Range(1,4));
    }
     void Update()
    {  
        inGround = GroundTest();
        Jump();
        ChargeMana();
        ReflectShield();
        print(rbPlayer.velocity.x);
    }
    void FixedUpdate()
    {
        WallJump();
        if (moviment)
            Moviment();
    }
    void Moviment(){
        if (rbPlayer.velocity.y > 12)
        {
            //limit aleatory big jumps
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x,12);
        }
        velocity_x = Input.GetAxisRaw("Horizontal");
        rbPlayer.velocity = new Vector2(velocity_x * speed,rbPlayer.velocity.y);
        switch (velocity_x)
        {
            case 1:
                frontRight = true;
                break;
            case-1:
                frontRight = false;
                break;

        }
    }
    void Jump()
    {
        if(Input.GetButton("Jump") && inGround)
        {
            outPutSound.PlayOneShot(sound[Random.Range(0,4)]);
            rbPlayer.AddForce( Vector2.up * forceJump);
            inGround = false;
        }
        if (Input.GetButtonUp("Jump") && rbPlayer.velocity.y >= 0)
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x,0);
    }
    bool GroundTest()
    {
        olReturn = Physics2D.OverlapCircleAll(new Vector2(transform.position.x,transform.position.y - groundTestP), radius);
        foreach (Collider2D col in olReturn)
        {
            if (col.tag == "Ground")
                return true;
        }
        return false;
    }
    void ChargeMana()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine("ManaCount");
            moviment = false;
            rbPlayer.velocity = Vector2.zero;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            execute = true;
            moviment = true;
        }
    }
    void ReflectShield()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            clone = Instantiate(reflectShield, transform.position + new Vector3(0.5f, 0), Quaternion.identity,gameObject.transform);
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            Destroy(clone);
        }
    }
    private void OnDrawGizmos()
    {
        //Debug.DrawLine(transform.position + new Vector3(0.5f,-0.28f), transform.position + new Vector3(0.5f,0.28f),new Color(1,1,1));
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y - groundTestP), radius);
     //Debug.DrawRay(transform.position - new Vector3(frontRight ?  - playerScaleX - 0.1f :  + playerScaleX + 0.1f,  + playerScaleY / 2), Vector2.up,Color.green);

    }
    IEnumerator ManaCount()
    {
        if (execute)
        {
            manaCount = Time.time;
            execute = false;
        }
        yield return mana = manaCount - Time.time;
    }
    private void WallJump()
    {
     wallTest = Physics2D.Raycast(transform.position - new Vector3(frontRight ? -playerScaleX/2 - 0.1f : playerScaleX/2 + 0.1f, -playerScaleY / 2 + 0.3f), Vector2.up, playerScaleY);
        if (wallTest && !inGround)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                rbPlayer.AddForce(new Vector2(0, 600));
   

        }
        if (rbPlayer.velocity.y < 0 && wallTest)
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, -1.5f);
    }
      
}
