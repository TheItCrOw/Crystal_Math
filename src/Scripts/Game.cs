using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {


    public static float highScore = 0;
    public Text highScoreText;

	void Start ()
    {


        DontDestroyOnLoad(this.gameObject);



	}
	



	void Update ()
    {


        highScoreText.text = "Highscore: " + highScore;

	}
}
