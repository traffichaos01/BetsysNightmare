using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class betsyController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //CheckCollision();
	}

    //void CheckCollision()
    //{

    //}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Projectile")
        {
            Destroy(col.gameObject);
            Debug.Log("We had a hit");
        }
    }
}
