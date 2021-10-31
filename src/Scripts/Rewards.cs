using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewards : MonoBehaviour {


    public float speed;
    public float deathTime;

	void Start ()
    {
		
	}



	void Update ()


    {

        transform.position = Vector2.MoveTowards(transform.position, transform.position + new Vector3(0,5,0), speed);

        Destroy(gameObject, deathTime);


    }
}
