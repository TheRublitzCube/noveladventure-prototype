//Player Controller Script for Main Character
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    Rigidbody2D rb; //Player's rigid body object

    /*Enum for allowing player to do certain actions
        
        CanDoAll = Player has complete control
        CanNotJump = Player can move but cannot jump
        CanNotMove = Player can jump but cannot move
        CanDoNone = Player input is completely disabled

    */
    public enum InputState { CanDoAll, CanNotJump, CanNotMove, CanDoNone };
    public InputState currentInputState;

    //Movement Variable
    public float movementSpeed; //movement speed

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //if player can input to some capacity
        if (currentInputState != InputState.CanDoNone)
        {
            Move();
        }
    }
    
    //Function for basic character movement
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
            Debug.Log("Player jumped");
        }

        else
        {
            Debug.Log("Player cannot jump");
        }
    }
}
