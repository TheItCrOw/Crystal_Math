using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour {


    //public GameObject ApplyForce;
    public GameObject deathEffect;
    public string tagName1;
    public string tagName2;


    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update () {


       transform.localScale += new Vector3(0.05f, 0f, 0.05f);


    }


    private void OnCollisionEnter(Collision collision)
    {
        {
            //Call everything that happens when the enemy hits the crystal.

            //Everything that happens when the problem is correct

            if (collision.gameObject.tag == tagName1 || collision.gameObject.tag == tagName2)
            {

              Instantiate(deathEffect, collision.transform.position, Quaternion.identity);
              Destroy(collision.gameObject);

            }
        }


    }
}
