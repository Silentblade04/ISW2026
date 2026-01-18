using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class SpawnerScript : MonoBehaviour
{
    /*[SerializeField] private GameObject enemyTemplate;
    [SerializeField] private GameObject enemyTemplate2;*/
    public GameObject player { get { return playerPrefab; } }

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private float spawnDelay;
    [SerializeField] private float spawnDelay2;

    public int score;
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;


    [SerializeField] private GameObject[] enemyTemplates = new GameObject[1]; //list of templates
    private int[] spawnPositions = new int[10]; //# of enemies in each collum

    private List<GameObject> enemies = new List<GameObject>();
    void Start()
    {
        spawnDelay = 1;
        spawnDelay = 5;
        enemies.Clear();
    }
    void Update()
    {
        scoreText.text = "Score: " + score;
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (spawnDelay > 0)
        {
            spawnDelay -= Time.deltaTime;
        }
        if (spawnDelay <= 0 && enemies.Count <= 24)
        {
            int i = Random.Range (0, 100);

            i += Mathf.RoundToInt(score/10f-10);
            if (i < 75)
            {
                spawnEnemy(enemyTemplates[0]);
                Debug.Log("Spawning Basic Enemy");
            }
            else if (i < 95)
            {
                int r = Random.Range(1, 100);
                if (r < 50)
                {
                    spawnEnemy(enemyTemplates[1]);
                }
                else
                {
                    spawnEnemy(enemyTemplates[2]);
                }
                Debug.Log("Spawning Intermediate Enemy");
            }
            else
            {
                spawnEnemy(enemyTemplates[3]);
                Debug.Log("Spawning Adv Enemy");
            }
        }    
    }

    private void spawnEnemy(GameObject template)
    {
        

            int i = Random.Range(0, spawnPositions.Length);

            if (spawnPositions[i] >= 4)
            {
                spawnDelay = 1;
                return;
            }

            spawnPositions[i] += 1;
        if (template == enemyTemplates[3])
        {
            spawnPositions[i] -= 1;
        }

            float randomX = Random.Range(-8.5f, 8.5f);

            GameObject enemy = Instantiate(template, new Vector3(randomX, transform.position.y + 0.5f, transform.position.z), template.transform.rotation);
            enemy.GetComponent<EnemyObj>().spawnedPosition(enemies.Count);

            enemies.Add(enemy);
            enemy.GetComponent<EnemyObj>().initalized(this, spawnPositions[i], i);

            spawnDelay = 1;
    }

    public void increaseScore(int scoreAmount)
    {
        score += scoreAmount;
    }
    public void killEnemy(GameObject enemy, int collumNumber)
    {
        enemies.Remove(enemy);
        spawnPositions[collumNumber] -= 1;

        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].GetComponent<EnemyObj>().CollumNumber == collumNumber)
            {
                enemies[i].GetComponent<EnemyObj>().offSet();
            }
        }

        Destroy(enemy);
        Debug.Log(enemies.Count);
    }
    
}
