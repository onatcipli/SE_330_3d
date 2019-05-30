using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject targetPrefab;

    public Transform spawnPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject clonedTarget = Instantiate(targetPrefab, spawnPoint.position, spawnPoint.rotation);
//            Destroy(clonedTarget, 10f);
        }
    }
}
