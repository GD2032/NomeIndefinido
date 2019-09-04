using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed,raycastDistance, raycastD;
   
    private Rigidbody2D rb;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Physics2D.Raycast(new Vector2(7.72f, 7.22f), Vector2.up, raycastDistance);
        Physics2D.Raycast(new Vector2(0.13f, 7.22f), Vector2.up, raycastDistance);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        rb.velocity = Vector2.right * speed;
    }
    private void TestFlip()
    {
        Physics2D.Raycast(transform.position + new Vector3(0.3f, 0), Vector2.right, raycastD);
    }
}
