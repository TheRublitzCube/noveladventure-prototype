//Player Controller Script for Main Character
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    Rigidbody2D rb; //Player's rigid body object

    //--------Input Allowances--------//
    /*
        0 - All or None
        1 - Move or Cant Move
        2 - Jump or Cant Jump
        3 - Shoot or Cant Shoot
     */
    public bool[] inputAllows = { true, true, true, true };

    //--------Movement Variables--------//
    public float movementSpeed; //movement speed
    public enum CharacterDirection { Up, UpRight, Right, DownRight, Down, DownLeft, Left, UpLeft, InPlace}; //directions character can face
    CharacterDirection currentCharacterDirection; //player's current dcirection

    //--------Jumping Variables--------//
    float airTimeCounter = 0; //time in the air
    bool grounded = true;
    bool hasJumped = false;

    //--------Shooting Variables--------//
    public GameObject BulletPrefab;
    float cooldownCounter = 0;
    bool coolingDown = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        currentCharacterDirection = CharacterDirection.Down;
    }

    // Update is called once per frame
    void Update()
    {
        // if player can input to some capacity
        if (inputAllows[0] == true)
        {
            Move();

            if (!coolingDown)
            {
                if (Input.GetButtonDown("Fire1") && inputAllows[3] == true) 
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

            if (!grounded)
            {
                airTimeCounter -= Time.deltaTime;

                if (airTimeCounter <= 0)
                {
                    grounded = true;
                    hasJumped = false;
                    inputAllows[3] = true;
                    Debug.Log("Player has landed");
                }
            
            }

        }
    }
    
    //--------Functions for basic character movement--------
    void Move()
    {
        if (Input.GetButtonDown("Jump") && inputAllows[2] == true) //if jump buton pressed and can jump
        {
            if (!hasJumped)
            {
                Jump();
                hasJumped = true;
                airTimeCounter = 0.77f;
                SetJumpDirection(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                inputAllows[3] = false; //prevent player from shooting in the air
            }
        }

        if (inputAllows[1] != false) //if player can move
        {
            rb.velocity = new Vector2(movementSpeed * Input.GetAxis("Horizontal"), movementSpeed * Input.GetAxis("Vertical")); //adds movement
        }

    }

    //--------Function for setting character jump direction--------
    void SetJumpDirection(float h, float v)
    {
        if (h > 0) //Character is moving to the right
        {
            if (v == 0)
            {
                currentCharacterDirection = CharacterDirection.Right;
                Debug.Log("Player is jumping to the right");
            }

            else if (v > 0)
            {
                currentCharacterDirection = CharacterDirection.UpRight;
                Debug.Log("Player is jumping to the upper right");
            }

            else if (v < 0)
            {
                currentCharacterDirection = CharacterDirection.DownRight;
                Debug.Log("Player is jumping to the lower right");
            }
        }

        else if (h < 0) //Character is moving to the left
        {
            if (v == 0)
            {
                currentCharacterDirection = CharacterDirection.Left;
                Debug.Log("Player is jumping to the left");
            }

            else if (v > 0)
            {
                currentCharacterDirection = CharacterDirection.UpLeft;
                Debug.Log("Player is jumping to the upper left");
            }

            else if (v < 0)
            {
                currentCharacterDirection = CharacterDirection.DownLeft;
                Debug.Log("Player is jumping to the lower left");
            }
        }

        else //Character isn't moving left or right
        {
            if (v == 0)
            {
                currentCharacterDirection = CharacterDirection.InPlace;
                Debug.Log("Player is jumping in place");
            }

            else if (v > 0)
            {
                currentCharacterDirection = CharacterDirection.Up;
                Debug.Log("Player is jumping forward");
            }

            else if (v < 0)
            {
                currentCharacterDirection = CharacterDirection.Down;
                Debug.Log("Player is jumping backward");
            }
        }
    }

    //Jumping Function
    void Jump()
    {
        grounded = false;
        Debug.Log("Player has jumped");
    }

    public bool IsGrounded()
    {
        return grounded;
    }

    //--------Combat Functions--------
    void Shoot()
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
