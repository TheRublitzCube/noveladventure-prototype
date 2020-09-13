//Player Controller Script for Main Character
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    Rigidbody2D rb; //Player's rigid body object

    /*--------Enum for allowing player to do certain actions--------
        
        CanDoAll = Player has complete control
        CanNotJump = Player can move but cannot jump
        CanNotMove = Player can jump but cannot move
        CanDoNone = Player input is completely disabled

    */
    public enum InputState { CanDoAll, CanNotJump, CanNotMove, CanDoNone };
    public InputState currentInputState;

    //--------Movement Variables--------//
    public float movementSpeed; //movement speed

    //--------Shooting Variables--------//
    public GameObject BulletPrefab;
    float cooldownCounter = 0;
    bool coolingDown = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // if player can input to some capacity
        if (currentInputState != InputState.CanDoNone)
        {
            Move();

            if (!coolingDown)
            {
                if (Input.GetButtonDown("Fire1")) 
                {
                    Shoot();
                    coolingDown = true;
                    cooldownCounter = 0.1f; 
                }
            }

            else
            {
                cooldownCounter -= Time.deltaTime;

                if (cooldownCounter <= 0)
                {
                    coolingDown = false;
                }
            }

        }
    }
    
    //--------Functions for basic character movement--------
    void Move()
    {
        if (Input.GetButtonDown("Jump")) //if jump buton pressed and can jump
        {
            Jump();
        }

        if (currentInputState != InputState.CanNotMove) //if player can move
        {
            rb.velocity = new Vector2(movementSpeed * Input.GetAxis("Horizontal"), movementSpeed * Input.GetAxis("Vertical")); //adds movement
        }

    }

    //Jumping Function
    void Jump()
    {
        if (currentInputState != InputState.CanNotJump)
        {
            //Debug.Log("Player jumped");
        }

        else
        {
            //Debug.Log("Player cannot jump");
        }
    }

    //--------Combat Functions--------
    void Shoot()
    {
        if (Input.GetButtonDown("Fire1") && !coolingDown)
        {
            //Debug.Log("Shot fired");
            Vector3 aimPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; //gets mouse position in relation to player
            aimPos.z = 0;
            aimPos.Normalize();

            GameObject bulletInstance = Instantiate(BulletPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            bulletInstance.GetComponent<Rigidbody2D>().AddForce(aimPos * 1000);
            Physics2D.IgnoreCollision(bulletInstance.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        }
    }
}
