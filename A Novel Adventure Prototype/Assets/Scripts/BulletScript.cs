using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float damage;
    public float bulletForce;
    float bulletSpeed = 0;
    Vector2 bulletVector = new Vector2();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(bulletSpeed * bulletVector.x * Time.deltaTime, bulletSpeed * bulletVector.y * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0);

        if (other.gameObject.GetComponent<EnemyBaseClass>() != null)
        {
            other.gameObject.GetComponent<EnemyBaseClass>().DealDamage(damage);
        }

        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void SetSpeedAndVector(float speed, Vector2 vector)
    {
        bulletSpeed = speed;
        bulletVector = vector;
    }

}
