//Course: GPE104 
//Prof: Matthew Henry 
//Student: Chad V

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic; // for List<>
using TMPro;
using System;

public class GameManager : MonoBehaviour
{   
    [Header("GameObjects")]
    public static GameManager someGameManager;
    public GameObject astroidPrefab;
    public GameObject bulletPrefab;
    public GameObject enemyPrefab;
    public ControllerPlayer controllerPlayer; // get controller ref for player entity.
    public TextMeshProUGUI someValue;
    public TextMeshProUGUI someStatusMsg;
    public TextMeshProUGUI someAstroidValue;
    public TextMeshProUGUI someBulletValue;
    public TextMeshProUGUI someStageValue;
    public TextMeshProUGUI someLivesValue;
    public TextMeshProUGUI someEndGameBulletValue;
    public TextMeshProUGUI someEndGameScoreValue;
    public TextMeshProUGUI someWinGameBulletValue;
    public TextMeshProUGUI someWinGameScoreValue;
    public Pawn someSpaceShipPawn;
    public GameObject someEndGameScreen;
    public GameObject someWinGameScreen;
    public Button someRestartButton;
    public Button someQuitButton;
    public Button someWinRestartButton;
    public Button someWinQuitButton;

    
    [Header("Lists")]
    public List<Obstacle> obstacleList;  // Obstacle list to keep track of astroids/obstacles
    public List<Bullet> bulletList; // A Bullet list to keep limit how many projectiles we spawn if the player holds down space.
    public List<float> someStageList;
    
    [Header("Int's")]
    public int someScore;
    public int enemyCount = 2;
    public int stage = 1;
    public int lives = 2;

    [Header("Floats")]
    public float someMaxWidth = Screen.width;
    public float someMaxHeight = Screen.height;
    public float someStageSecTime = 2f;
    public float ufoChancePercent = 70f;   // % chance each enemy is a UFO
    public float largeAsteroidScale = 2f;


    [Header("Bools")]
    public bool gameOver = false;
    public bool winGame = false;

    void Start()
    {
       InitGame();
    }

    void InitGame()
    {
        // StartCoroutine(StageOne(someStageSecTime));
        // StartCoroutine(StageTwo(someStageSecTime * 4));
        // StartCoroutine(StageThree(someStageSecTime * 8));
        // StartCoroutine(StageFour(someStageSecTime * 10));

        UpdateLivesUI();

        // Setup button listeners for endGame screen.
        if (someRestartButton != null)
        {
            someRestartButton.onClick.AddListener(OnRestartPressed);
        }
            
        if (someQuitButton != null)
        {
            someQuitButton.onClick.AddListener(QuitGame);
        }

        if (someWinRestartButton != null)
        {
            someWinRestartButton.onClick.AddListener(OnRestartPressed);
        }

        if (someWinQuitButton != null)
        {
            someWinQuitButton.onClick.AddListener(QuitGame);
        }

        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy();
        }        
    }
    //on awake(before start) if someGameManager is null assign gamemanger to this, and flag it to no destroy,
    //delete any duplicates. 
    public void Awake()
    {
        obstacleList = new List<Obstacle>();

        if (someGameManager == null)
        {
            someGameManager = this;
            DontDestroyOnLoad(gameObject);
            gameOver = false;
            winGame = false;
            Time.timeScale = 1f;
        }
        else
        {
            someGameManager.RebindFrom(this);
            enabled = false;
            Destroy(gameObject);
            return;
        }

        if (someEndGameScreen != null)
        {
            someEndGameScreen.gameObject.SetActive(false);
        }

        if (someWinGameScreen != null)
        {
            someWinGameScreen.gameObject.SetActive(false);
        }
    }

    public void RebindFrom(GameManager other)
    {
        // Scene references (all point at the new scene's objects)
        astroidPrefab = other.astroidPrefab;
        bulletPrefab = other.bulletPrefab;
        controllerPlayer = other.controllerPlayer;
        someValue = other.someValue;
        someStatusMsg = other.someStatusMsg;
        someAstroidValue = other.someAstroidValue;
        someBulletValue = other.someBulletValue;
        someStageValue = other.someStageValue;
        someLivesValue = other.someLivesValue;
        someSpaceShipPawn = other.someSpaceShipPawn;
        someEndGameScreen = other.someEndGameScreen;
        someWinGameScreen = other.someWinGameScreen;
        someRestartButton = other.someRestartButton;
        someQuitButton = other.someQuitButton;
        someWinRestartButton = other.someWinRestartButton;
        someWinQuitButton = other.someWinQuitButton;
        someEndGameScoreValue = other.someEndGameScoreValue;
        someEndGameBulletValue = other.someEndGameBulletValue;
        someWinGameScoreValue = other.someWinGameScoreValue;
        someWinGameBulletValue = other.someWinGameBulletValue;

        // Reset game state to the scene's Inspector defaults
        someScore = other.someScore;
        enemyCount = other.enemyCount;
        stage = other.stage;
        lives = other.lives;
        gameOver = false;
        winGame = false;
        Time.timeScale = 1f;
        someEndGameScoreValue.text = "Score: 0";
        someEndGameBulletValue.text = "Bullets Fired: 0";
        someWinGameScoreValue.text = someEndGameScoreValue.text;
        someWinGameBulletValue.text = someEndGameBulletValue.text;
        obstacleList = new List<Obstacle>();
        bulletList = new List<Bullet>();

        // Hide/Deactivae EndGame screen.
        if (someEndGameScreen != null)
        {
            someEndGameScreen.SetActive(false);
        }

        if (someWinGameScreen != null)
        {
            someWinGameScreen.SetActive(false);
        }

        InitGame();
    }

    void UpdateLivesUI()
    {
        if (someLivesValue != null)
        {
            someLivesValue.text = lives.ToString();
        }
    }

    IEnumerator StageOne(float someSeconds)
    {
        yield return new WaitForSeconds(someSeconds);
        enemyCount += 1;
        someStatusMsg.text = "Stage One Completed!";
        someStageValue.text = stage.ToString();
    }

    IEnumerator StageTwo(float someSeconds)
    {
        yield return new WaitForSeconds(someSeconds);
        enemyCount += 2;
        someStatusMsg.text = "Stage Two Completed!";
        stage = 2;
        someStageValue.text = stage.ToString();
    }

    IEnumerator StageThree(float someSeconds)
    {
        yield return new WaitForSeconds(someSeconds);
        enemyCount += 3;
        someStatusMsg.text = "Stage Three Completed!";
        stage = 3;
        someStageValue.text = stage.ToString();
    }

    IEnumerator StageFour(float someSeconds)
    {
        yield return new WaitForSeconds(someSeconds);
        enemyCount += 4;
        someStatusMsg.text = "OVERTIME STAGE!";
        stage = 4;
        someStageValue.text = stage.ToString();
    }
    
    void Update()
    {

        CleanupObstacleList();
        if (obstacleList != null && obstacleList.Count < enemyCount)
        {
            SpawnEnemy();
        }
        
        // if obstacle list is not null and obstacle count is less than 0 and controlerPlayer is not null, if Game over does not equal
        // false and controllerPlayer.somePawn is not null, print Victory to the debug log, and set gameOver to true;
        if (obstacleList != null)
        {

            Debug.Log($"Astroid Count: {obstacleList.Count}");

            if (obstacleList.Count <= 0 && controllerPlayer != null)
            {
                if (gameOver == false && winGame == false && controllerPlayer.somePawn != null)
                {
                    Debug.Log("Victory!!");
                    winGame = true;
                }
                
            }
            someAstroidValue.text = obstacleList.Count.ToString();
        }

        // if (bulletList != null)
        // {
        //     //Debug.Log($"Bullet Count: {bulletList.Count}");
        //     someBulletValue.text = bulletList.Count.ToString();
        // }
   

        // if game over is false and controller player is not null, and controllerPlayer.pawn is NULL, then fail as it means
        // our player died. 

        if (gameOver == false && controllerPlayer != null && controllerPlayer.somePawn != null)
        {
            if (!controllerPlayer.somePawn.gameObject.activeSelf)
            {
                lives--;
                UpdateLivesUI();
                if (lives > 0)
                {
                    controllerPlayer.somePawn.transform.position = Vector3.zero;
                    controllerPlayer.somePawn.gameObject.SetActive(true);
                    Health h = controllerPlayer.somePawn.GetComponent<Health>();
                    if (h != null) h.InstantHeal();                    
                }
                else
                {
                    //show end game screen w/buttons, pause game.
                    Debug.Log("FAILURE!!");
                    gameOver = true;
                }

            }
        }

        if (someValue != null)
        {
            someValue.text = "" + someScore;
        }

        // if (gameOver)
        // {
        //     //show end game screen
        //     someEndGameScoreValue.text = $"Score: {someValue.text}";
        //     someEndGameBulletValue.text = $"Bullets Fired: {someBulletValue.text}";
        //     someEndGameScreen.gameObject.SetActive(true);
        //     // stop game time so the game doesn't keep running. 
        //     Time.timeScale = 0f;
        // }
        // if (winGame)
        // {
        //     someWinGameScoreValue.text = $"Score: {someValue.text}";
        //     someWinGameBulletValue.text = $"Bullets Fired: {someBulletValue.text}";
        //     someWinGameScreen.gameObject.SetActive(true);
        //     Time.timeScale = 0f;
        // }
    }

    public void OnRestartPressed()
    {
        Debug.Log("RESTARTING....");
        Time.timeScale = 1f;
        someEndGameScreen.gameObject.SetActive(false);
        LoadScene("Level1");
    }

    public void QuitGame()
    {

        LoadScene("MainMenu");
        // Application.Quit();

        // // If running inside the Unity Editor
        // #if UNITY_EDITOR
        // UnityEditor.EditorApplication.isPlaying = false;
        // #endif
    }

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

   void CleanupObstacleList()
   {
        // Remove null entries from the list
        // item => item == null: This is a lambda expression acting as a condition:
        // item: Represents the current object being evaluated in the list.
        // =>: The lambda operator (read as "goes to").
        // item == null: The check being performed.
        obstacleList.RemoveAll(item => item == null);
   }

    //Enemy spawning
    void SpawnEnemy()
    {
        if (astroidPrefab == null) return;

        // Find a spawn point away from the player
        Vector3 spawnPos = Vector3.zero;
        float minDistance = 50f;
        bool safeSpawn = false;
        while (!safeSpawn)
        {
            float someXRange = UnityEngine.Random.Range(-50f, 50f);
            float someYRange = UnityEngine.Random.Range(10f, 50f);
            float someZRange = UnityEngine.Random.Range(-100f, 100f);
            spawnPos = new Vector3(someXRange, someYRange, someZRange);

            if (controllerPlayer != null && controllerPlayer.somePawn != null)
            {
                float distance = Vector3.Distance(spawnPos, controllerPlayer.somePawn.transform.position);
                if (distance >= minDistance)
                {
                    safeSpawn = true;
                }
            }
            else
            {
                safeSpawn = true;
            }
        }

        // Roll the dice: UFO or large asteroid?
        bool isUfo = enemyPrefab != null && UnityEngine.Random.Range(0f, 100f) < ufoChancePercent;

        GameObject prefabToSpawn = isUfo ? enemyPrefab : astroidPrefab;
        GameObject newEnemy = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
        newEnemy.transform.localScale = new Vector3(UnityEngine.Random.Range(0.5f, 5f), UnityEngine.Random.Range(0.5f, 5f), UnityEngine.Random.Range(0.5f, 5f));

        if (!isUfo)
        {
            // Large asteroid. AsteroidBreak reads this scale in its Start().
            newEnemy.transform.localScale = Vector3.one * largeAsteroidScale;
        }

        // Track it so the win condition counts it
        Obstacle obstacle = newEnemy.GetComponent<Obstacle>();
        if (obstacle != null)
        {
            obstacleList.Add(obstacle);
        }

        // Only asteroids have this; for UFOs it's simply null.
        AsteroidBreak asteroidBreakScript = newEnemy.GetComponent<AsteroidBreak>();
        if (asteroidBreakScript != null)
        {
            asteroidBreakScript.astroidPrefab = astroidPrefab;
        }
}
}
