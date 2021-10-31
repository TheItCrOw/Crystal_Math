using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


	

	void Update ()
    {

	}


    void JumpMovement()
    {
        //Wir checken ob die Touches auf dem Screen min. 1 ist.
        if (Input.touchCount > 0) 
        {
            Touch touch = Input.GetTouch(0);

            //speiuchern den Touch in eienr Variable
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);


            //Zecihnen der Linie
            Debug.DrawLine(transform.position, touchPosition, Color.red);


            //Keine Z ACHSE!
            touchPosition.z = 0f;

            //Teleportiert den spieler zur tocuhed position
            transform.position = touchPosition; 
        }
    }
}
