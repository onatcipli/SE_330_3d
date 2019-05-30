using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class Target : MonoBehaviour
{
    public float health = 50f;
    
    public float speed = 3f;

    private void Start()
    {
        // Destroy(gameObject, 5f);
    }

    private void Update()
    {
        moveAndRotateToTarget();
        if (health < 0)
        {
            Destroy(gameObject);
        }
    }


    public void moveAndRotateToTarget()
    {
        Transform playerTransform = GameObject.FindWithTag("player").GetComponent<Transform>();
        
        transform.rotation = Quaternion.Lerp(transform.rotation,
            Quaternion.LookRotation(playerTransform.position - transform.position),
            15 * Time.deltaTime
        );

        transform.position += transform.forward * speed * Time.deltaTime;
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            DieObject();
        }
    }

    private void DieObject()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("bullet"))
        {
            health -= 10f;
            Debug.Log("health :  " + health);
        }
    }
}