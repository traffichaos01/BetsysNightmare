using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class betsyController : MonoBehaviour {

    public float health;
    public Slider hpBar;
    public GameObject missile;
    public Rigidbody2D betsy;
    public Vector2 missileVelocity;
    public int pelletCount = 0;
    public Text Name;
    public int slaughtered;

	// Use this for initialization
	void Start () {
        health = 100.0f;
        hpBar.value = health;
        Debug.Log(health);
        InvokeRepeating("pelletCounter", 5.0f, 0.5f);
        nameBetsy();
	}

    void nameBetsy()
    {
        if (vars.sacrifices == 0)
        {
            Name.text = "Betsy";
        } else if (vars.sacrifices == 1)
        {
            Name.text = "Daequan";
        }
        else if (vars.sacrifices == 2)
        {
            Name.text = "Daequan";
        }
        else if (vars.sacrifices == 3)
        {
            Name.text = "Daequan";
        }
        else if (vars.sacrifices == 4)
        {
            Name.text = "Daequan";
        }
        else
        {
            Name.text = "Cow #" + vars.sacrifices;
        }
    }
	
	// Update is called once per frame
	void Update () {
	}

    void pelletCounter()
    {
        pelletCount += 1;
        shootPelletsOne();
        if (pelletCount % 2 == 0)
        {
            betsy.transform.Translate(Vector3.right * Time.deltaTime);
        }
        else
        {
            betsy.transform.Translate(Vector3.left * Time.deltaTime);
        }
    }



    void shootPelletsOne()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 betsyLocation = betsy.transform.position;
            Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(-200.0f, 200.0f));
            GameObject newMissile = Instantiate(missile, betsyLocation, rotation);
            newMissile.GetComponent<Rigidbody2D>().velocity = newMissile.transform.up * (5 + (5 * vars.sacrifices));
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Projectile")
        {
            health -= 2.5f;
            hpBar.value = health;
            Destroy(col.gameObject);
            if (hpBar.value <= 2.5f)
            {
                Debug.Log("You won");
                vars.sacrifices += 1;
                SceneManager.LoadScene("Slaughter", LoadSceneMode.Single);
            }
        }
    }
}
