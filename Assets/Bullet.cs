using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage = 10f;
    
    void Start()
    {
        Destroy(gameObject, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void setBulletDamage(float d = 5f)
    {
        damage = d;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("target"))
        {
            Destroy(gameObject);
        }
    }
}
