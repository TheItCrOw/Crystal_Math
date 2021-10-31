using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {


    [Header("Game Over")]
    public GameObject GameOver;
    public GameObject EndScore;
    public GameObject retryButton;



    [Header("You did it")]
    public GameObject GoodJob;
    public GameObject YouDidIt;
    public GameObject retryButtonGood;


    void Start ()
    {
		


	}
	
	// Update is called once per frame
	void Update ()
    {


	}


    public IEnumerator EndScreen()
    {

        GameOver.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        EndScore.SetActive(true);
        yield return new WaitForSeconds(1);
        retryButton.SetActive(true);
    }



    public IEnumerator levelDoneScreen()
    {

        GoodJob.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        YouDidIt.SetActive(true);
        yield return new WaitForSeconds(1);
        retryButtonGood.SetActive(true);
    }


    public void RetryScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
