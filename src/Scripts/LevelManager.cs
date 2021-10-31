using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    private bool isAlive = true;

    [Header("GameObject")]
    public GameObject enemyPrefabs3;
    public GameObject enemyPrefabs2;
    public GameObject enemyPrefabs1;
    public GameObject player;
    private Animator playerAnim;
    public GameObject[] playerGraphics;
    public GameObject waveExplosion;
    public GameObject deathExplosion;

    [Header("Spawn Values")]
    private float diffculty;    //1 is easy, 3 is rank level
    private float spawnRateE = 3;
    private int lastSpawnObj;
    private float score = 0;
    private int health = 8;
    private float maxScore = -10;

    [Header("Spawn Points")]
    public GameObject[] spawns;


    [Header("Time Management")]
    public float slowDownFactor = 0.05f;
    public float slowDownLength = 2f;

    [Header("Touch/Mouse Controll")]
    public GameObject goodOnClickEffect;
    public GameObject badOnClickEffect;
    public GameObject goodReward;
    public GameObject badReward;

    [Header("UI")]
    public GameObject Interface;
    public TextMesh ScoreText;

    public GameObject coolDownGraphic;
    private float coolDown = 22;
    public GameObject specialAttack;
    public GameObject specialUIEffect;
    public GameObject errorSound;

    public Text endScore;
    public GameObject EndScreen;
    public GameObject levelMenu;
    public UIController uiController;
    public GameObject ExplainField;
    public Text explainText;
    public GameObject[] explainButtons;
    public GameObject leavePlayingLevel;

    public Text highScore;



    private void Awake()
    {
        Application.targetFrameRate = 30;
    }


    void Start()

    {


        highScore.text = ("Highscore: " + PlayerPrefs.GetFloat("HighScore", 0));

        if(isAlive)
        {
            //activate the player
            playerGraphics[0].SetActive(true);

            //Get the player Animator
            playerAnim = player.GetComponent<Animator>();

            //Set the cooldown
            coolDownGraphic.transform.localScale = new Vector3(0.1f, 0.1f);
            specialUIEffect.SetActive(false);

        }



    }



    void Update()
    {

        if(isAlive)
        {
            //GameTimer decreases over time
            spawnRateE -= Time.deltaTime;

            //Handle the difficultys
            HandleDifficulty();


            //For the slowMo Effect
            Time.timeScale += (1f / slowDownLength) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);

            //The mouse touch Controll
            mouseTouchControl();


            //Timer for special Attack
            if (coolDown > 0)
            {
                specialUIEffect.SetActive(false);
                coolDownGraphic.SetActive(true);
                coolDownGraphic.transform.localScale += new Vector3(0.04f, 0.04f) * Time.deltaTime;
                coolDown -= Time.deltaTime;
            }

            if (coolDown < 0)
            {
                coolDownGraphic.SetActive(false);
                specialUIEffect.SetActive(true);
            }

            //if score is -100, then kms
            if (score < -90)
                TakeDamage(1);
        }

    }


    void HandleDifficulty()
    {
        //Set the difficulty
        if (diffculty == 3)
        {
            //rank level
            SpawnRate(1, enemyPrefabs3);
            maxScore = 999999;
        }
        if (diffculty == 2)
        {
            //medium level
            SpawnRate(2, enemyPrefabs2);
            maxScore = 300;
        }
        if (diffculty == 1)
        {
            //easy level
            SpawnRate(3, enemyPrefabs1);
            maxScore = 150;
        }
        //If the score hits the max score, kill my self
        if (maxScore == score)
        {
            TakeDamage(99);
        }



    }


    void mouseTouchControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Enemy")
                {
                    Instantiate(goodOnClickEffect, hit.transform.position, Quaternion.identity);
                    Instantiate(goodReward, hit.transform.position, Quaternion.identity);
                    Destroy(hit.transform.gameObject);
                }
                if (hit.transform.tag == "Friend")
                {
                    Instantiate(badOnClickEffect, hit.transform.position, Quaternion.identity);
                    Instantiate(badReward, hit.transform.position, Quaternion.identity);
                    HandleScore(-20);
                    Destroy(hit.transform.gameObject);
                }

                if (hit.transform.tag == "Special" && coolDown < 0)
                {
                    Instantiate(specialAttack, new Vector3(0, 0, 0), specialAttack.transform.rotation);
                    coolDownGraphic.transform.localScale = new Vector3(0.1f, 0.1f);
                    coolDown = 22;
                }

                if (hit.transform.tag == "Special" && coolDown > 0)
                {
                    Instantiate(errorSound, hit.transform.position, Quaternion.identity);
                    Debug.Log("Attack isnt ready yet");
                }
            }
        }
    }



    void SpawnRate(float spawnRate, GameObject enemyPrefab)
    {


        if (spawnRateE < 0) 
        {
            GameObject spawnObj;
            Vector2 spawnPos;
            spawnObj = spawns[Random.Range(0, spawns.Length)];

            if (lastSpawnObj != System.Array.IndexOf(spawns, spawnObj))
            {
                lastSpawnObj = System.Array.IndexOf(spawns, spawnObj);
                spawnPos = spawnObj.transform.position;

                Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
                spawnRateE = spawnRate;

            }

        }



    }

    void TakeDamage(int damage)
    {

        health -= damage;
        playerAnim.SetTrigger("gotHit");

        if (health == 6)
        {
            for (int i = 0; i < playerGraphics.Length - 1; i++)
            {
                playerGraphics[i].SetActive(false);
                playerGraphics[1].SetActive(true);
                Instantiate(waveExplosion, new Vector3(0, 0, -1), Quaternion.identity);
            }
        }

        if (health == 4)
        {
            for (int i = 0; i < playerGraphics.Length - 1; i++)
            {
                playerGraphics[i].SetActive(false);
                playerGraphics[2].SetActive(true);
                Instantiate(waveExplosion, new Vector3(0, 0, -1), Quaternion.identity);
            }
        }

        if (health == 2)
        {
            for (int i = 0; i < playerGraphics.Length - 1; i++)
            {
                playerGraphics[i].SetActive(false);
                playerGraphics[3].SetActive(true);
                Instantiate(waveExplosion, new Vector3(0, 0, -1), Quaternion.identity);
            }
        }

        if(health == 0 || health < 0 && health > 10)
        {
            isAlive = false;
            Instantiate(deathExplosion, new Vector3(0, 0, -1), Quaternion.identity);
            player.SetActive(false);
            Interface.SetActive(false);
            StartCoroutine(uiController.EndScreen());
            endScore.text = "Score: " + score;
            EndScreen.SetActive(true);

            //PlayerPrefs saves data on local Com
            if (score > PlayerPrefs.GetFloat("HighScore"))
            {
                PlayerPrefs.SetFloat("HighScore", score);
            }

        }

        if(health < -15)
        {
            isAlive = false;
            Instantiate(deathExplosion, new Vector3(0, 0, -1), Quaternion.identity);
            player.SetActive(false);
            Interface.SetActive(false);
            StartCoroutine(uiController.levelDoneScreen());
            EndScreen.SetActive(true);

            //PlayerPrefs saves data on local Com
            if (score > PlayerPrefs.GetFloat("HighScore"))
            {
                PlayerPrefs.SetFloat("HighScore", score);
            }
        }
    }


    void DoSlowMotion()
    {
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;

    }


    void HandleScore(int amount)
    {

        score += amount;

        ScoreText.text = score.ToString();
    }


    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// //ACTIVATED BY BUTTON


    public void SetDifficulty_1()       //Easy
    {
        diffculty = 1;
        levelMenu.SetActive(false);
        Interface.SetActive(true);
        leavePlayingLevel.SetActive(true);
    }


    public void SetDifficulty_2()       //Medium
    {
        diffculty = 2;
        levelMenu.SetActive(false);
        Interface.SetActive(true);
        leavePlayingLevel.SetActive(true);

    }


    public void SetDifficulty_3()       //Rank Level
    {
        diffculty = 3;
        levelMenu.SetActive(false);
        Interface.SetActive(true);
        leavePlayingLevel.SetActive(true);

    }



    public void explainDiff_1()
    {

        ExplainField.SetActive(false);

        explainText.text = (
                            "Difficulty: Starting! \n" +
                            " \n" +
                            "Train at a slow pace. Suited for people who either play for the first time or are in a beginner state of mental arithmetic...or both. \n" +
                            "Goal: 150 Points!"
                            );

        for (int i = 0; i < explainButtons.Length; i++)
        {
            explainButtons[i].SetActive(false);
            explainButtons[0].SetActive(true);
        }

        ExplainField.SetActive(true);
    }




    public void explainDiff_2()
    {
        ExplainField.SetActive(false);

        explainText.text = ("Difficult: Above Average \n" +
                            " \n" +
                            "Train at a challenging pace. If -Getting Started- is too easy, this is the stairway that leads you to rank level!\n" +
                            "\n" +
                            "Goal: 300 Points!");

        for (int i = 0; i < explainButtons.Length; i++)
        {
            explainButtons[i].SetActive(false);
            explainButtons[1].SetActive(true);
        }

        ExplainField.SetActive(true);
    }




    public void explainDiff_3()
    {
        ExplainField.SetActive(false);

        explainText.text = ("Difficulty: Rank Level \n" +
                            " \n" +
                            "The grind for the highest score begins here! Challenge your friends and beat your own highscores. But be aware: Rank Level requires high skills! \n" +
                            "Goal: no limit!");

        for (int i = 0; i < explainButtons.Length; i++)
        {
            explainButtons[i].SetActive(false);
            explainButtons[2].SetActive(true);
        }

        ExplainField.SetActive(true);
    }


    public void LoadMainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void LeavePlayingLevel()
    {
        score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
