using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    [Header("Enemy Controll")]
    public float moveSpeed;

    [Header("Digits")]
    public GameObject TextPrefab;
    private TextMesh textMesh;
    public float spawnRate;
    public float destroyDigits;

    [Header("On Hit")]
    public GameObject goodImpact;
    public GameObject badImpact;
    public GameObject goodReward;
    public GameObject badReward;
    private bool isTrue;


    private void Awake()
    {
        //Get the text component to write
        textMesh = TextPrefab.transform.Find("TextMesh").GetComponent<TextMesh>();
        isTrue = true;

    }

    void Start()
    {

        GenerateProblem();

    }



    void Update()
    {

        linearMovement();

        //OnHitCrystal();

    }


    void linearMovement()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(0, 0), moveSpeed);
    }


    void GenerateProblem()
    {

        //Generate a random number between 1 and 10 for each parameter
        float _a = Random.Range(1, 10);

        float _b = Random.Range(1, 10);

        //Generate a random digit for random choose of operator
        int _random = Random.Range(1, 5);

        float _sum;

        if (_random == 1)
        {
            _sum = _a + _b;
            int __random = Random.Range(1, 3);
            if (__random == 1)
            {
                _sum += Random.Range(1, 10);
                isTrue = false;
                transform.tag = ("Enemy");

            }

            StartCoroutine(SpawnDigits(_a.ToString(), "+", _b.ToString(), "=", _sum.ToString()));
        }

        if (_random == 2)
        {
            _sum = _a - _b;
            int __random = Random.Range(1, 3);
            if (__random == 1)
            {
                _sum -= Random.Range(1, 10);
                isTrue = false;
                transform.tag = ("Enemy");
            }

            StartCoroutine(SpawnDigits(_a.ToString(), "-", _b.ToString(), "=", _sum.ToString()));
        }

        if (_random == 3)
        {
            _sum = _a * _b;
            int __random = Random.Range(1, 3);
            if (__random == 1)
            {
                _sum += Random.Range(1, 15);
                isTrue = false;
                transform.tag = ("Enemy");
            }

            StartCoroutine(SpawnDigits(_a.ToString(), "*", _b.ToString(), "=", _sum.ToString()));
        }

        if (_random == 4)
        {
            _sum = _a / _b;
            int __random = Random.Range(1, 3);
            if (__random == 1)
            {
                _sum -= Random.Range(1, 15);
                isTrue = false;
                transform.tag = ("Enemy");
            }

            StartCoroutine(SpawnDigits(_a.ToString(), ":", _b.ToString(), "=", _sum.ToString("F1")));
        }



    }


    IEnumerator SpawnDigits(string a, string op, string b, string equals, string sum)
    {

        yield return new WaitForSeconds(0.4f);

        textMesh.text = a;
        GameObject _textPrefab = (GameObject)Instantiate(TextPrefab, transform.position, Quaternion.identity);
        Destroy(_textPrefab, destroyDigits);


        yield return new WaitForSeconds(spawnRate);

        textMesh.text = op;
        GameObject _textPrefab2 = (GameObject)Instantiate(TextPrefab, transform.position, Quaternion.identity);
        Destroy(_textPrefab2, destroyDigits);


        yield return new WaitForSeconds(spawnRate);

        textMesh.text = b;
        GameObject _textPrefab3 = (GameObject)Instantiate(TextPrefab, transform.position, Quaternion.identity);
        Destroy(_textPrefab3, destroyDigits);


        yield return new WaitForSeconds(spawnRate);

        textMesh.text = equals;
        GameObject _textPrefab4 = (GameObject)Instantiate(TextPrefab, transform.position, Quaternion.identity);
        Destroy(_textPrefab4, destroyDigits);


        yield return new WaitForSeconds(spawnRate);

        textMesh.text = sum;
        GameObject _textPrefab5 = (GameObject)Instantiate(TextPrefab, transform.position, Quaternion.identity);
        Destroy(_textPrefab5, destroyDigits);



    }


    private void OnCollisionEnter(Collision collision)
    {
        {
            //Call everything that happens when the enemy hits the crystal.

            //Everything that happens when the problem is correct

            if (collision.gameObject.tag == "LevelManager")
            {
                if (isTrue)
                {
                    collision.gameObject.SendMessage("HandleScore", 10);
                    Instantiate(goodImpact, new Vector2(0, 0), Quaternion.identity);
                    Instantiate(goodReward, new Vector2(0, 0), Quaternion.identity);
                    Destroy(gameObject);

                }

                else
                {
                    Instantiate(badImpact, new Vector2(0, 0), Quaternion.identity);
                    Instantiate(badReward, new Vector2(0, 0), Quaternion.identity);
                    collision.gameObject.SendMessage("TakeDamage", 1);
                    Destroy(gameObject);
                }
            }
        }


    }
}
