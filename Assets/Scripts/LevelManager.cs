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
    public Transform secondSpawnPoint;

    public Canvas startCanvas;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        amoutTime = RemoteConfigManager.levelTime;
        startCanvas.enabled = true;
        audioSource.Stop();
    }

    public void startLevel()
    {
        StopAllCoroutines();
        startCanvas.enabled = false;
        level = 1;
        currentLevelTime.text = level + " level time : " + amoutTime.ToString();
        StartCoroutine(levelManage());
    }

    IEnumerator levelManage()
    {
        for (int i = 0; i < 15; i++)
        {
            yield return StartCoroutine(levelTime(amoutTime));
            yield return StartCoroutine(manageRemaningTime());
            level++;
        }
    }

    IEnumerator manageRemaningTime()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        GameObject[] gameObjectsWithTag = GameObject.FindGameObjectsWithTag("target");
        foreach (GameObject currentTarget in gameObjectsWithTag)
        {
            Destroy(currentTarget);
        }

        for (int i = 10; i > 0; i--)
        {
            currentLevelTime.text = "remaining time is : " + i;
            yield return new WaitForSeconds(1);
        }

        yield break;
    }

    IEnumerator levelTime(int time)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }

        bool randomSpawn = true;
        int level_time = time;
        currentLevelTime.text = level + "level time : " + level_time.ToString();
        for (int i = 0; i < time; i++)
        {
            level_time--;
            if (level_time % 4 == 0)
            {
                if (randomSpawn)
                {
                    spawnTarget(secondSpawnPoint);
                    randomSpawn = !randomSpawn;
                }
                else
                {
                    spawnTarget(spawnPoint);
                }
            }
            else if (level_time % level - 15 == 0)
            {
                if (randomSpawn)
                {
                    spawnTarget(secondSpawnPoint);
                    randomSpawn = !randomSpawn;
                }
                else
                {
                    spawnTarget(spawnPoint);
                }
            }

            currentLevelTime.text = level + "level time : " + level_time.ToString();
            yield return new WaitForSeconds(1);
        }

        Debug.Log("Corutine done!!!");
        yield break;
    }

    void spawnTarget(Transform spawnPoint)
    {
        GameObject clonedTarget = Instantiate(targetPrefab, spawnPoint.position, spawnPoint.rotation);
        Target target = clonedTarget.GetComponent<Target>();
        float improveTargetSpeed = 1 + level * 0.10f;
        float improveTargetHealth = 1 + level * 0.2f;
        float scaleTargetAccordingToHealth = level * 0.15f;
        clonedTarget.transform.localScale += new Vector3(scaleTargetAccordingToHealth, scaleTargetAccordingToHealth,
            scaleTargetAccordingToHealth);
        target.speed = target.speed * improveTargetSpeed;
        target.health = target.health * improveTargetHealth;
    }
}