using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullBehaviour : MonoBehaviour
{
    RaycastHit2D WallTest;
    [SerializeField] LayerMask wall;
    bool direction;
    void Update()
    { 
        WallTest = Physics2D.Raycast(transform.position, direction ? Vector2.left:Vector2.right,1, wall);
        if (WallTest.collider == null) { }
        else if (WallTest.collider.gameObject.tag == "Wall")
            direction = !direction;
        transform.position += new Vector3((direction ? -2f : 2) * Time.deltaTime, 0);
     
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
              direction = !direction;
        }
        //print ("a"
        //if (collision.collider.gameObject.tag == "Enemy")

    }
}
