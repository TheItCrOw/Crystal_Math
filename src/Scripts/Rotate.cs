using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {


    public Vector3 rotationAxes;
    public float rotationSpeed;


	void Start () {
		
	}
	

	void Update () {


        transform.Rotate(rotationAxes, rotationSpeed * Time.deltaTime);

	}
}
