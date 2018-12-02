using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour {

    public float moveHorizontal;
    public float moveVertical;
    public float speed;
    public float newSpeed;
    public float bulletSpeed;
    public float boostTimer = 0;
    public float boostCooldown = 0;
    public float missileTimer = 0;

    public Rigidbody2D player;
    private Vector2 movement;
    public Vector3 weaponPosition;

    public GameObject weapon;
    public GameObject missile;
    public Slider EnergyBar;


    // Use this for initialization
    void Start()
    {
        InvokeRepeating("CleaverReload", 1.0f, 0.25f);
        InvokeRepeating("boostAction", 1.0f, 0.25f);
        directPlayer();
        instantiateCleaver();
    }

    void instantiateCleaver()
    {
        Vector3 weaponPosition = new Vector3(
            player.transform.position.x + 0.1f,
            player.transform.position.y + 0.2f,
            player.transform.position.z
            );
        var newCleaver = Instantiate(weapon, weaponPosition, player.transform.rotation);
    }

    void Update()
    {
        movementController();
    }

    void movementController()
    {
        directPlayer();
        movePlayer();
        moveCleaver();
        UpdateEnergy();
    }

    void UpdateEnergy()
    {
        if (EnergyBar.value < 10.0f)
        {
            EnergyBar.value += 0.05f;
        }
    }

    void directPlayer()
    {
        var direction = trackMouse();
        transform.up = direction;
    }

    Vector2 trackMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
        );
        return direction;
    }

    void movePlayer()
    {
        newSpeed = speed * 20;
        moveHorizontal = Input.GetAxis("Horizontal") * speed;
        moveVertical = Input.GetAxis("Vertical") * speed;
        movement = new Vector3(moveHorizontal, moveVertical);
        var regularMovement = movement * newSpeed * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift) && boostCooldown == 0)
        {
            if (Mathf.Approximately(player.velocity.x, 0) && Mathf.Approximately(player.velocity.y, 0))
            {
                Debug.Log("MOVE FATTY");
            }
            else
            {
                boostTimer = 1.0f;
                boostCooldown = 1.0f;
                EnergyBar.value -= 4.0f;
            }
        }
        if (boostTimer > 0)
        {
            //if (player.velocity.y > 5.0f || player.velocity.y < -5.0f || player.velocity.x > 5.0f || player.velocity < -5.0f)
            //{
            //    player.velocity = regularMovement * 2;
            //}
            //else
            //{
                player.velocity = regularMovement * 3;
            //}
        }
        else if (boostTimer == 0)
        {
            player.velocity = regularMovement;
        }
    }

    void moveCleaver()
    {
        var Cleaver = GameObject.Find("Cleaver(Clone)");
        Cleaver.transform.parent = player.transform;
        Cleaver.transform.up = player.transform.up;

        if (Input.GetKeyDown(KeyCode.Mouse0) && missileTimer == 0 && EnergyBar.value >= 2.0f)
        {
            shootCleaver();
        }
        else
        {
            Cleaver.transform.position = transform.TransformPoint(0.05f, 0.1f, 0);
        }
    }

    void shootCleaver()
    {
        var Cleaver = GameObject.Find("Cleaver(Clone)");
        Vector3 CleaverPosition = Cleaver.transform.position;
        GameObject newMissile = Instantiate(missile, CleaverPosition, player.transform.rotation);
        newMissile.GetComponent<Rigidbody2D>().velocity = bulletSpeed * Time.deltaTime * trackMouse().normalized;
        missileTimer = 1.0f;
        EnergyBar.value -= 2.0f;
    }

    void CleaverReload()
    {
        if (missileTimer > 0)
        {
            missileTimer -= 1f;
        }
        else if (missileTimer < 0)
        {
            missileTimer = 0;
        }

        if (boostCooldown > 0)
        {
            boostCooldown -= 0.25f;
        }
        else if (boostCooldown == 0)
        {
            boostCooldown = 0;
        }
    }

    void boostAction()
    {
        if (boostTimer > 0)
        {
            boostTimer -= 1f;
            Debug.Log(boostTimer);
        }
        else if (boostTimer < 0)
        {
            boostTimer = 0;
        }
    }
}

