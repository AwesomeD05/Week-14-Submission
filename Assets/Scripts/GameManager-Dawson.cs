using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject enemyOnePrefab;
    public GameObject enemyTwoPrefab;
    public GameObject cloudPrefab;
    public GameObject coinPrefab;
    public GameObject powerupPrefab;
    public GameObject healthPrefab;
    public GameObject audioPlayer;
    public GameObject gameOverText;
    public GameObject restartText;

    public AudioClip coinSound;
    public AudioClip gainLifeSound;
    public AudioClip powerUpSound;
    public AudioClip powerDownSound;

    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI powerupText;

    public float horizontalScreenSize;
    public float verticalScreenSize;
    public int score;
    public bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        horizontalScreenSize = 10f;
        verticalScreenSize = 6.5f;
        score = 0;
        gameOver = false;
        AddScore(0);
        Instantiate(playerPrefab, new Vector3(0, -1.6f, 0), Quaternion.identity);

        CreateSky();
        InvokeRepeating("CreateEnemyOne", 1, 3);
        InvokeRepeating("CreateEnemyTwo", 5, 10);
        InvokeRepeating("CreateCoin", 5, 10);
        StartCoroutine(SpawnPowerup());
        StartCoroutine(SpawnHealth());
        powerupText.text = "No Powers yet!";
    }

    IEnumerator SpawnPowerup()
    {
       float spawnTime = Random.Range(6f,8f);
       yield return new WaitForSeconds(spawnTime);
       CreatePowerup();
       StartCoroutine(SpawnPowerup());
    }
    IEnumerator SpawnHealth()
    {
        float spawnTime = Random.Range(3f, 5f);
        yield return new WaitForSeconds(spawnTime);
        CreateHealth();
        StartCoroutine(SpawnHealth());
    }
    void Update()
    {
        if (gameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void CreateEnemyOne()
    {
        Instantiate(enemyOnePrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * 0.9f, verticalScreenSize, 0), Quaternion.Euler(180, 0, 0));
    }

    void CreateEnemyTwo()
    {
        Instantiate(enemyTwoPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * 0.9f, verticalScreenSize, 0), Quaternion.Euler(180, 0, 0));
    }

    void CreateSky()
    {
        for (int i = 0; i < 30; i++)
        {
            Instantiate(cloudPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize), Random.Range(-verticalScreenSize, verticalScreenSize), 0), Quaternion.identity);
        }

    }
    
    void CreateCoin()
    {
        Instantiate(coinPrefab, new Vector3(Random.Range(-horizontalScreenSize * 0.8f, horizontalScreenSize * 0.8f), Random.Range(-verticalScreenSize * 0.8f, 0), 0), Quaternion.identity);
    }

    void CreatePowerup()
    {
        Instantiate(powerupPrefab, new Vector3(Random.Range(-horizontalScreenSize * 0.8f, horizontalScreenSize * 0.8f), Random.Range(-verticalScreenSize * 0.8f, 0), 0), Quaternion.identity);
    }

    void CreateHealth()
    {
        Instantiate(healthPrefab, new Vector3(Random.Range(-horizontalScreenSize * 0.8f, horizontalScreenSize * 0.8f), Random.Range(-verticalScreenSize * 0.8f, 0), 0), Quaternion.identity);
    }
    public void ManagePowerupText(int powerupType)
    {
        switch (powerupType)
        {
            case 1:
                powerupText.text = "Speed Boost!";
                break;
            case 2:
                powerupText.text = "Double Shot!";
                break;
            case 3:
                powerupText.text = "Triple Shot!";
                break;
            case 4:
                powerupText.text = "Shield Activated!";
                break;
            default:
                powerupText.text = "No Powers yet!";
                break;
        }
    }
    public void PlaySound(int soundType)
    {
        AudioSource audioSource = audioPlayer.GetComponent<AudioSource>();
        switch (soundType)
        {
            case 1:
                audioSource.PlayOneShot(powerUpSound);
                break;
            case 2:
                audioSource.PlayOneShot(powerDownSound);
                break;
            case 3:
                audioSource.PlayOneShot(gainLifeSound);
                break;
            case 4:
                audioSource.PlayOneShot(coinSound);
                break;
            default:
                break;
        }
    }
    public void AddScore(int earnedScore)
    {
        score = score + earnedScore;
        scoreText.text = "Score: " + score;
    }
    public void ChangeLivesText (int currentLives)
    {
        livesText.text = "Lives: " + currentLives;
    }
    public void GameOver()
    {
        gameOverText.SetActive(true);
        restartText.SetActive(true);
        gameOver = true;
        CancelInvoke();
    }
}
