using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralController : MonoBehaviour
{
    public float healthAmount = 100f;
    public float currentHealth;

    public int bulletNumber = 0;

    public int coin = 0;

    public float lastShootTime;

    public bool isGamePaused = false;

    public Canvas killedPlayerCanvas;

    /*
    private float shootTimer = 0f;
    private bool didShoot = false;
    */

    [Header("Unity Referances")] public Image healthBar;
    public Text canvasNumberOfBulletTextRef;
    public Text canvasCoinTextRef;

    public Rigidbody targetPrefab;
    public Transform createPosition;

    [Header("Player Movment")] public Joystick _joystick;
    public Joystick _crossJoystick;

    public float speed = 5f;
    public float jumpHeight = 10f;
    public float snowFriction = 1.5f;

    [Header("Player shoot")] public Rigidbody bulletPrefab;

    public Transform bulletCross;

    public Transform changePlace;

    public Canvas startCanvas;


    public LevelManager levelManager;

    void Start()
    {
        healthAmount = RemoteConfigManager.playerHealthAmount;
        killedPlayerCanvas.enabled = false;
        aboutHealthStart();
        aboutBulletStart();
        aboutCoinStart();
    }

    void Update()
    {
        bulletText();
        coinText();
        handePausePlay();
        handleMakeLivePlayer();
        handleKillPlayer();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Instantiate(targetPrefab, createPosition.position, createPosition.rotation);
            //shoot();
        }

        if (Input.GetKey(KeyCode.F))
        {
            shootBullet();
        }

        if (healthBar.fillAmount <= 0)
        {
            Debug.Log("kill Player here!");
        }
    }


    void crossHandle()
    {
        bulletCross.eulerAngles = new Vector3(0,
            0, Mathf.Atan2(_crossJoystick.Vertical, _crossJoystick.Horizontal) * 180 / Mathf.PI);

        if (_crossJoystick.Horizontal > 0.5 || _crossJoystick.Vertical > 0.5)
        {
            shootBullet();
        }
        else if (_crossJoystick.Horizontal < -0.5 || _crossJoystick.Vertical < -0.5)
        {
            shootBullet();
        }


//        bulletCross.localEulerAngles = bulletCross.eulerAngles;
    }

    private void FixedUpdate()
    {
        crossHandle();
        playerMovment();
    }


    public void handlePlayAgainButton()
    {
        transform.position = new Vector3(14, 1.5f, 5);
        killedPlayerCanvas.enabled = false;
        var gameObjects = GameObject.FindGameObjectsWithTag("target");
        foreach (var currentTarget in gameObjects)
        {
            Destroy(currentTarget);
        }

        bulletDamage = 10f;
        aboutHealthStart();
        aboutBulletStart();
        aboutCoinStart();
        startCanvas.enabled = true;
        levelManager.StopAllCoroutines();
    }

    void handleMakeLivePlayer()
    {
        if (healthBar.fillAmount > 0)
        {
            isGamePaused = false;
            killedPlayerCanvas.enabled = false;
        }
    }

    void handleKillPlayer()
    {
        if (healthBar.fillAmount <= 0)
        {
            killedPlayerCanvas.enabled = true;
            isGamePaused = true;
        }
    }

    void handePausePlay()
    {
        if (isGamePaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    void lookAtTheMousePosition()
    {
//        Vector3 screenToViewportPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);
//        Debug.Log(screenToViewportPoint);
//        changePlace.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5);

        Vector3 screenToViewportPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bulletCross.transform.rotation = Quaternion.Lerp(transform.rotation,
            Quaternion.LookRotation(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z) -
                                    bulletCross.transform.position),
            50 * Time.deltaTime
        );

//        Vector3 screenToViewportPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//        bulletCross.transform.rotation = Quaternion.Lerp(transform.rotation,
//            Quaternion.LookRotation(Input.mousePosition - bulletCross.transform.position),
//            50 * Time.deltaTime
//        );   

//        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//        Debug.Log(Input.mousePosition);
    }

    void playerMovment()
    {
        var horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        var verticalMove = Input.GetAxisRaw("Vertical") * jumpHeight;

        transform.Translate(new Vector3(horizontalMove, verticalMove, 0) * Time.deltaTime);


        // JOYSTICK CONTROLLER
        var joystickHorizontalMove = _joystick.Horizontal * speed;
        var joystickVerticalMove = _joystick.Vertical * jumpHeight;


        transform.Translate(new Vector3(joystickHorizontalMove, joystickVerticalMove, 0) * Time.deltaTime);
    }

    void shootBullet()
    {
        if (bulletNumber > 0)
        {
            if (Time.time - lastShootTime > 0.5f)
            {
                lastShootTime = Time.time;
                setBullet(-1);
                Rigidbody bulletInstance;
                bulletInstance = Instantiate(bulletPrefab, bulletCross.position, bulletCross.rotation) as Rigidbody;
                bulletInstance.GetComponent<Bullet>().setBulletDamage(bulletDamage);
                bulletInstance.AddForce(bulletCross.right * 1500);
            }
        }
        else
        {
            Debug.Log("no bullet");
        }
    }

    private float bulletDamage = 10f;

    public void increaseBulletDamage(float amount = 5f)
    {
        if (coin >= 50)
        {
            setCoin(-50);
            bulletDamage += amount;
        }
    }


    void setBulletDamage(Bullet bullet)
    {
        bullet.setBulletDamage(bulletDamage);
    }


    void aboutCoinStart()
    {
        coin = RemoteConfigManager.coinStartAmount;
    }

    void coinText()
    {
        String canvasCoinText = "Coin : " + coin.ToString() + "$";
        canvasCoinTextRef.text = canvasCoinText;
    }

    public void setCoin(int amount)
    {
        coin += amount;
    }

    void aboutHealthStart()
    {
        currentHealth = healthAmount;
        healthBar.fillAmount = currentHealth / healthAmount;
    }

    public void setHealth(float amount)
    {
        currentHealth += amount;
        // and update the healthBar
        healthBar.fillAmount = currentHealth / healthAmount;
    }

    void aboutBulletStart()
    {
        bulletNumber = 100;
    }

    void bulletText()
    {
        String canvasBulletText = "Number of bullets : " + bulletNumber.ToString();
        canvasNumberOfBulletTextRef.text = canvasBulletText;
        // if I need this text somewhere else we can return the text
        // return canvasBulletText;
    }

    public void setBullet(int amount)
    {
        bulletNumber += amount;
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.collider.name);
        if (other.collider.CompareTag("target"))
        {
            if (healthAmount > 0)
            {
                setHealth((-5));
            }
        }

        if (other.collider.CompareTag("snow"))
        {
            speed -= snowFriction;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag("snow"))
        {
            speed += snowFriction;
        }
    }
}