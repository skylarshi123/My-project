using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //properties of Rigidbody, AddForce,velocity
    Rigidbody rb;
    [SerializeField] GameObject bullet;
    float speed = 10;

    [SerializeField] float bulletSpeed = 100;
    bool onGround = false;
    bool doubleJump = false;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMove(InputValue value) //Vector 2 --> (x,y)
    {
        Vector2 input = value.Get<Vector2>();

        Debug.Log(input);
        //transform.position, transform rotation
        //transform.forward, transform.right (to get where player is facing)
        rb.velocity = input.y * transform.forward + input.x * transform.right;
        rb.velocity *= speed;

    }

    void OnFire() //function when fire action  triggered
    {
        Debug.Log("FIRING");
        GameObject bulletInstance = Instantiate(bullet, transform.position + 0.5f * transform.forward, Quaternion.identity);
        Rigidbody bulletRigidbody = bulletInstance.GetComponent<Rigidbody>();

        bulletRigidbody.AddForce(bulletSpeed * transform.forward);
    }

    void OnJump()
    {

        if (onGround || doubleJump)
        {
            doubleJump = !doubleJump;
            Debug.Log("JUMP");
            rb.velocity = transform.up * speed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            //our feet are touching the ground
            onGround = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            //our feet are touching the ground
            onGround = false;
        }
    }
}
