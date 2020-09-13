using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0);
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
