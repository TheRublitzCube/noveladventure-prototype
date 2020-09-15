using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorHoleScript : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<PlayerControllerScript>().IsGrounded())
            {
                Debug.Log("Player walked onto hole");
            }

            else
            {
                Debug.Log("Player is jumping over hole");
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<PlayerControllerScript>().IsGrounded())
            {
                Debug.Log("Player is in hole");
            }

            else
            {
                Debug.Log("Player is over hole");
            }
        }
    }
}
