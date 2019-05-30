using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public float lastMoveTime;
    public Text currentLevelTime;
    public int amoutTime;
    public int level;

    public GameObject targetPrefab;

    public Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        amoutTime = RemoteConfigManager.levelTime;
        level = 1;
        currentLevelTime.text = level + " level time : " + amoutTime.ToString();

        StartCoroutine(levelManage());
    }

    IEnumerator levelManage()
    {
        for (int i = 0; i < 2; i++)
        {
            Debug.LogError("level: " + level);
            level++;
            yield return StartCoroutine(levelTime(amoutTime));
        }
    }

    IEnumerator levelTime(int time)
    {
        int level_time = time;
        currentLevelTime.text = "level time : " + level_time.ToString();
        for (int i = 0; i < time; i++)
        {
            level_time--;
            if (level_time % 2 == 0)
            {
                spawnTarget();
            }

            currentLevelTime.text = "level time : " + level_time.ToString();
            yield return new WaitForSeconds(1);
        }

        Debug.Log("Corutine done!!!");
        yield break;
    }

    void spawnTarget()
    {
        GameObject clonedTarget = Instantiate(targetPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}