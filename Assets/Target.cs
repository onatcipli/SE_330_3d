using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    public float health = 50f;

    public float speed = 3f;

    private GeneralController generalController;

    private void Start()
    {
        generalController = GameObject.FindWithTag("player").GetComponent<GeneralController>();
        // Destroy(gameObject, 5f);

//        InvokeRepeating("randomMovment", 0, 2f);
    }

    private void Update()
    {
        moveToPlayerWithRandomMovments();
        if (health < 0)
        {
            DieObject();
        }
    }

    private float lastRandomMoveTime;
    
    
    void moveToPlayerWithRandomMovments()
    {
        float timeSinceLastMove = Time.time - lastRandomMoveTime;

        if (timeSinceLastMove < 2)
        {
            Debug.Log("random movment !!!!!!");
            randomMovment();
            lastRandomMoveTime = Time.time;
        }
        else
        {
            moveAndRotateToTarget();
        }
    }

    public void randomMovment()
    {
        Transform playerTransform = GameObject.FindWithTag("player").GetComponent<Transform>();

        Vector3 randomPosition = new Vector3(Random.Range(-100.0f, 100.0f), playerTransform.position.y, playerTransform.position.z);

        transform.rotation = Quaternion.Lerp(transform.rotation,
            Quaternion.LookRotation(randomPosition - transform.position),
            40 * Time.deltaTime
        );

        transform.position += transform.forward * 7f * Time.deltaTime;
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
        generalController.setCoin(5);
        Debug.Log("Current coin" + generalController.coin);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("bullet"))
        {
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            health -= bullet.damage;
            Debug.Log("health :  " + health);
        }
    }
}