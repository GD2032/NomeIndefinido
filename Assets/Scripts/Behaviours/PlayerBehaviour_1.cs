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
    [SerializeField] private float speed, forceJump, radius, groundTestP,wallTestP,wallTestD,wallTestDY; //P = position; D = distance
    [SerializeField] private float mana, manaCount;
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
        speed = 6;
        forceJump = 12;
        radius = 0.2f;
        groundTestP = 0.2f;
        ui.Fades(true,1,Random.Range(1,4));
    }
     void Update()
    {  
        inGround = GroundTest();
        Jump();
        ChargeMana();
        ReflectShield();
    }
    void FixedUpdate()
    {
        if(moviment)
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
            outPutSound.PlayOneShot(sound[Random.Range(0,4)]);
            rbPlayer.velocity = Vector2.up * forceJump;
            inGround = false;
        }
        print(rbPlayer.velocity.y);
        if (Input.GetButtonUp("Jump") && rbPlayer.velocity.y >= 0)
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x,0);
    }
    bool GroundTest()
    {
        //wallTest = Physics2D.Raycast(new Vector2(transform.position.x + wallTestP, transform.position.y), Vector2.up, wallTestD);

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
}
