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
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{   
    [Header("DEBUG")]
    public Key showHighScore = Key.F1;

    [Header("GameObjects")]
    public static GameManager someGameManager;
    public GameObject astroidPrefab;
    public GameObject bulletPrefab;
    public GameObject enemyPrefab;
    public GameObject astronautPrefab;
    public ControllerPlayer controllerPlayer; // get controller ref for player entity.
    public Pawn someSpaceShipPawn;
    public GameObject someEndGameScreen;
    public GameObject someWinGameScreen;
    public Button someRestartButton;
    public Button someQuitButton;
    public Button someWinRestartButton;
    public Button someWinQuitButton;
    public GameObject ufoPrefab;

    [Header("Text/GUI")]
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
    public TextMeshProUGUI someAstronautValue;
    
    [Header("Lists")]
    public List<Obstacle> obstacleList;  // Obstacle list to keep track of astroids/obstacles
    public List<Bullet> bulletList; // A Bullet list to keep limit how many projectiles we spawn if the player holds down space.
    public List<float> someStageList;
    
    [Header("Int's")]
    public int bulletsFired = 0;
    public int someScore;
    public int enemyCount = 1;
    public int stage = 1;
    public int lives = 2;
    public int astronautsPickedUp = 0;
    public int astronautCount = 2;
    public int ufoCount = 1;
    public int fwdThrustLevel;
    public int aftThrustLevel;
    public int rightThrustLevel;
    public int leftThrustLevel;


    [Header("Floats")]
    public float someMaxWidth = Screen.width;
    public float someMaxHeight = Screen.height;
    public float someStageSecTime = 2f;
    public float ufoChancePercent = 30f;   // % chance each enemy is a UFO
    public float largeAsteroidScale = 10f;


    [Header("Bools")]
    public bool gameOver = false;
    public bool winGame = false;

    [Header("Spawn Area")]
    public Vector3 spawnCenter = new Vector3(0f, 0f, 0f);
    public Vector3 spawnSize = new Vector3(20f, 5f, 20f);
    public Vector3 spawnPos; 

    void Start()
    {
       InitGame();
       fwdThrustLevel = 0;
       aftThrustLevel = 0;
       rightThrustLevel = 0;
       leftThrustLevel = 0;
    }

    void InitGame()
    {
        StartCoroutine(StageOne(someStageSecTime));
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

    void Update()
    {
        // debug high score.
        if (Keyboard.current[showHighScore].wasPressedThisFrame)
        {
           Debug.Log($"HighScore from PlayerPrefs: {PlayerPrefs.GetInt("HighScore", 0)}");
        }        

        CleanupObstacleList();
        if (obstacleList != null && obstacleList.Count < enemyCount)
        {
            SpawnEnemy();
        }
        
        // if obstacle list is not null and obstacle count is less than 0 and controlerPlayer is not null, if Game over does not equal
        // false and controllerPlayer.somePawn is not null, print Victory to the debug log, and set gameOver to true;
        if (obstacleList != null)
        {

            if (obstacleList.Count <= 0 && controllerPlayer != null)
            {
                if (gameOver == false && winGame == false && controllerPlayer.somePawn != null)
                {
                    CheckHighScore();
                    Debug.Log("Victory!!");
                    winGame = true;
                }
                
            }
            someAstroidValue.text = obstacleList.Count.ToString();
        }
   

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
                    CheckHighScore();
                    Debug.Log("FAILURE!!");
                    gameOver = true;
                }

            }
        }

        if (someValue != null)
        {
            someValue.text = "" + someScore;
        }

        if (someAstronautValue != null)
        {
            someAstronautValue.text = astronautsPickedUp.ToString();
        }

        if (someBulletValue != null)
        {
           someBulletValue.text = bulletsFired.ToString();
        }

        if (gameOver)
        {
            //show end game screen
            someEndGameScoreValue.text = $"Score: {someValue.text}";
            someEndGameBulletValue.text = $"Bullets Fired: {someBulletValue.text}";
            someEndGameScreen.gameObject.SetActive(true);
            // stop game time so the game doesn't keep running. 
            Time.timeScale = 0f;
        }
        if (winGame)
        {
            someWinGameScoreValue.text = $"Score: {someValue.text}";
            someWinGameBulletValue.text = $"Bullets Fired: {someBulletValue.text}";
            someWinGameScreen.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }    

    Vector3 RandomSpawnPosition()
    {
        Vector3 half = spawnSize * 0.5f;
        float x = UnityEngine.Random.Range(spawnCenter.x - half.x, spawnCenter.x + half.x);
        float y = UnityEngine.Random.Range(10f, 15f);
        float z = UnityEngine.Random.Range(spawnCenter.z - half.z, spawnCenter.z + half.z);
        return new Vector3(x, y, z);
    }    

    public void OnAstronautPickedUp()
    {
        someScore += 1000;
        someStatusMsg.text = "Saved 1 Astronaut, 1000pts earned!";
        astronautsPickedUp++;
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
        someAstronautValue = other.someAstronautValue;

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
        someAstronautValue.text = "0";
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
        enemyCount = 0;
        astronautCount = 0;

        someStatusMsg.text = "Stage One Started!";
        someStageValue.text = stage.ToString();
    }

    // IEnumerator StageTwo(float someSeconds)
    // {
    //     yield return new WaitForSeconds(someSeconds);
    //     enemyCount += 2;
    //     astronautCount += 2;
    //     someStatusMsg.text = "Stage Two Completed!";
    //     stage = 2;
    //     someStageValue.text = stage.ToString();
    // }

    // IEnumerator StageThree(float someSeconds)
    // {
    //     yield return new WaitForSeconds(someSeconds);
    //     enemyCount += 3;
    //     astronautCount += 3;
    //     someStatusMsg.text = "Stage Three Completed!";
    //     stage = 3;
    //     someStageValue.text = stage.ToString();
    // }

    // IEnumerator StageFour(float someSeconds)
    // {
    //     yield return new WaitForSeconds(someSeconds);
    //     enemyCount += 4;
    //     astronautCount += 4;
    //     someStatusMsg.text = "OVERTIME STAGE!";
    //     stage = 4;
    //     someStageValue.text = stage.ToString();
    // }
    


   void CheckHighScore()
   {
        int best = PlayerPrefs.GetInt("HighScore", 0);
        Debug.Log($"Current high score: {best}, Current score: {someScore}");

        if (someScore > best)
        {
           Debug.Log($"New high score! Saving {someScore}");
           PlayerPrefs.SetInt("HighScore", someScore);
           PlayerPrefs.Save();
           someStatusMsg.text = "NEW HIGH SCORE! GJ";
        }
        else
        {
           Debug.Log($"Score {someScore} not higher than high score {best}");
        }
    }

    public void OnRestartPressed()
    {
        Debug.Log("RESTARTING....");
        Time.timeScale = 1f;
        someEndGameScreen.gameObject.SetActive(false);
        stage = 1;
        LoadScene("TestLevel");
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

        // Spawn astronaut pickups
        if (astronautPrefab != null)
        {
            for (int i = 0; i < astronautCount; i++)
                {
                    Vector3 astronautSpawnPos = RandomSpawnPosition();

                    Instantiate(astronautPrefab, astronautSpawnPos, Quaternion.identity);
                }
        }

        // if no astroid prefab, return
        if (astroidPrefab == null) return;

        // Find a spawn point away from the player
        spawnPos = Vector3.zero;
        float minDistance = 10f;
        bool safeSpawn = false;

        // check so that we dont sapwn astroids on top of the player instantly killing them. 
        while (!safeSpawn)
        {
            spawnPos = RandomSpawnPosition();

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
        bool isUfo = ufoPrefab != null && UnityEngine.Random.Range(0f, 100f) < ufoChancePercent;

        GameObject prefabToSpawn = isUfo ? enemyPrefab : astroidPrefab;
        GameObject newEnemy = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);

        // DO NOT resize/scale UFOs/enemyFighters. 
        if (!isUfo){
            newEnemy.transform.localScale = new Vector3(UnityEngine.Random.Range(0.5f, 5f), UnityEngine.Random.Range(0.5f, 5f), UnityEngine.Random.Range(0.5f, 5f));    
        }
        

        if (astroidPrefab)
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
