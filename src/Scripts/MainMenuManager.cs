using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {


    public NativeShare nativeShare;

    [Header("Highscore")]
    private float highScore;
    private AudioSource audioSrc;
    public AudioClip transitionClip;
    public Animator menuAnim;

    [Header("UI Elements")]
    public Text highScoreText;
    public GameObject LicensesText;
    public GameObject PrivacyText;
    public GameObject how;
    public GameObject muteSoundIcon;
    public GameObject MainMenu;
    public GameObject Settings;
    public GameObject KMBProductions;

    public GameObject[] tutorialElements;
    private int currentIndex;



    public void Start ()
    {
        //Get the audioSource
        audioSrc = GetComponent<AudioSource>();

        //Get the highscore of the saved playerPrefs
        highScore = PlayerPrefs.GetFloat("HighScore", 0);
        highScoreText.text = "Highscore: " + highScore;

        SetTutorial();


        //Create the share button
        CreateNativeShare();
	}
	



    //Load the Level Scene
    public IEnumerator LoadLevelSceneCoroutine()
    {
        menuAnim.SetTrigger("loadLevel");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("Level");
    }



    //The method that is called on the Coroutine to load the Level Scene
    public void LoadLevelScene()
    {
        audioSrc.clip = transitionClip;
        audioSrc.Play();

        StartCoroutine(LoadLevelSceneCoroutine());
    }



    //(FOR SHARING OIN SOCIAL MEDIA
    public void CreateNativeShare()
    {
        nativeShare.SetSubject("");
        nativeShare.SetText("I just achieved a new Highscore!");
        //nativeShare.AddFile("Assets/Plugins/NativeShare/ShareScreen");
        nativeShare.SetTitle("Wow! A Highscore of: " + highScore);
        //nativeShare.SetTarget("");
    }

   

    private void SetTutorial()
    {
        //Turn off all tutorial Elemnts except for the first
        for (int i = 0; i < tutorialElements.Length; i++)
        {
            tutorialElements[i].SetActive(false);
            tutorialElements[0].SetActive(true);
        }
        currentIndex = 0;
    }

          
    //BUTTONS
    //Quits the gam
    public void QuitGame()
    {
        Application.Quit();
    }

    //Opens the how to play text
    public void HowButton()
    {
        how.SetActive(true);
        SetTutorial();
    }

    //Goes back to the main Menu
    public void MainMenuButton()
    {
        how.SetActive(false);
        Settings.SetActive(false);
    }

    //Turns on/off all sound
    public void ToggleSound()
    {
        if(AudioListener.pause != true)
        {
            AudioListener.pause = true;
            muteSoundIcon.SetActive(true);
            return;
        }


        if(AudioListener.pause == true)
        {
            AudioListener.pause = false;
            muteSoundIcon.SetActive(false);
            return;
        }


    }


    //Open the Settings
    public void OpenSettings()
    {
        Settings.SetActive(true);
    }


    //Email Pop up on the app
    public void SendEmail()
    {
        string email = "keboen@web.de";
        string subject = MyEscapeURL("My Subject");
        string body = MyEscapeURL("My Body\r\nFull of non-escaped chars");
        Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
    }
    string MyEscapeURL(string url)
    {
        return WWW.EscapeURL(url).Replace("+", "%20");
    }


    //Share on social Media
    public void ShareOnSocialmedia()
    {

        nativeShare.Share();
    }

    //Toggle the licenses Text
    public void LicenseText()
    {
        LicensesText.SetActive(!LicensesText.activeSelf);
    }


    //Toggle the privacy Text
    public void PirvacyText()
    {
        PrivacyText.SetActive(!PrivacyText.activeSelf);
    }


    //Toggle the kmb window
    public void KMBWindow()
    {
        KMBProductions.SetActive(!KMBProductions.activeSelf);
    }


    //Manage the tutorial
    public void ManageTutorialForward()
    {

        //First: Get the current array index of which part is open
        //Second disable the curretn page and enable the next

        for (int i = 0; i < tutorialElements.Length; i++)
        {
            if (tutorialElements[i].gameObject.activeInHierarchy)
            {
                currentIndex = i;
                //Debug.Log(currentIndex + " is active");
            }

        }


        if (currentIndex != tutorialElements.Length - 1)
        {
            tutorialElements[currentIndex].SetActive(false);
            currentIndex++;
            tutorialElements[currentIndex].SetActive(true);
        }
        else
        {
            Debug.Log("The Array is out of the index, im sorry");
        }


    }


    //Manage the tutorial
    public void ManageTutorialBackwards()
    {

        //First: Get the current array index of which part is open
        //Second disable the curretn page and enable the next


        for (int i = 0; i < tutorialElements.Length; i++)
        {
            if (tutorialElements[i].gameObject.activeInHierarchy)
            {
                currentIndex = i;
                //Debug.Log(currentIndex + " is active");
            }

        }


        if(currentIndex != 0)
        {
            tutorialElements[currentIndex].SetActive(false);
            currentIndex--;
            tutorialElements[currentIndex].SetActive(true);
        }
        else
        {
            Debug.Log("The Array is out of the index, im sorry");
        }



    }

}
