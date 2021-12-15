using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner eSpawn;
    public float maxSpawnX, minSpawnX;
    private Vector3 camBottomLeft, camTopRight;
    public Camera cam;

    [SerializeField]
    private GameObject[] enemies;
    [SerializeField]
    private float spawnTime = 2.0f;

    public List<GameObject> spawnedEnemies, deadEnemies;
    
    [SerializeField]
    private int maxEnemies = 10;

    [SerializeField]
    public int totalEnemies = 40;
    public int enemiesSpawned = 0;
    public bool allEnemiesDead = false;
    public int deadEnemiesCount = 0;

    public Text scoreText;

    public PlayerM9 player;

    public SlideIn[] sliders;

    private bool started = false;

    private int slidFor = 0;

    void Awake(){
        eSpawn = this;
        cam = gameObject.GetComponentInParent<Camera>();
        camBottomLeft = cam.ViewportToWorldPoint(new Vector3(0,0,0));
        camTopRight = cam.ViewportToWorldPoint(new Vector3(1,1,0));
        minSpawnX = camBottomLeft.x;
        maxSpawnX = camTopRight.x;
        // Debug.Log("MinSpawnX = "+minSpawnX+"MaxSpawnX = "+maxSpawnX);
    }

    public void ManualStart()
    {
        enemiesSpawned = 0;
        allEnemiesDead = false;
        spawnedEnemies = new List<GameObject>();
        deadEnemies = new List<GameObject>();
        InvokeRepeating("SpawnEnemy", 2.0f, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        //  if(!started && StateManager.smInstance.IsPlaying()){
        //      started = true;
        //      ManualStart();
        //  }
        // SetMaxAndMin();
        //Start polling to see when all enemies are dead, but we only need to 
        //check after all of the enemies have been spawned
        //if(enemiesSpawned == totalEnemies){
            // Debug.Log("Dead1");
            if(deadEnemies.Count < totalEnemies){
                // Debug.Log("Dead2");
                foreach(GameObject enemyObj in spawnedEnemies){
                    // Debug.Log("Dead? = "+enemyObj.GetComponent<Enemy>().isDead());
                    if(enemyObj.GetComponent<Enemy>().isDead()){
                        deadEnemiesCount++;
                        scoreText.text = deadEnemiesCount.ToString();
                        deadEnemies.Add(enemyObj);
                    }
                }
                foreach(GameObject deadEnemy in deadEnemies){
                    // Debug.Log("Dead3");
                    //Remove each dead enemy from the spawned enemies list so we don't keep re-looping through the dead ones
                    spawnedEnemies.Remove(deadEnemy);
                }
            }
            else{
                allEnemiesDead = true;
            }

        if(enemiesSpawned>0 && deadEnemiesCount%10 == 0 && slidFor != deadEnemiesCount){
            int choose = (int)Random.Range(0,2);
            sliders[choose].SlideMeIn();
            slidFor = deadEnemiesCount;
        }
       // }
    }

    void SetMaxAndMin(){
        camBottomLeft = cam.ViewportToWorldPoint(new Vector3(0,0,0));
        camTopRight = cam.ViewportToWorldPoint(new Vector3(1,1,0));
        minSpawnX = camBottomLeft.x;
        maxSpawnX = camTopRight.x;
        // Debug.Log("maxSPawnX = "+maxSpawnX+" and minspawnX = "+minSpawnX);
    }

    void SpawnEnemy(){
        // Debug.Log("Hello1?");
        if(!player.dead){
            if(spawnedEnemies.Count < maxEnemies && enemiesSpawned < totalEnemies){
                // Debug.Log("Hello2?");
                int index = Random.Range (0, enemies.Length);
                GameObject enemy = enemies[index];
                float xPos = Random.Range(minSpawnX, maxSpawnX);
                GameObject newEnemy = Instantiate(enemy);
                if(newEnemy.GetComponent<FlyingEnemy>()){
                    newEnemy.transform.position = new Vector3(xPos, newEnemy.transform.position.y, 0);
                    // Debug.Log("Hello3?");
                }
                else{
                    newEnemy.transform.position = new Vector3(xPos, gameObject.transform.position.y, 0);
                }
                Debug.Log("Spawning "+newEnemy.name+" at pos "+xPos);
                // if(xPos>0){
                //     Debug.Log("Spawning "+newEnemy.name+" at pos "+xPos+" and flipping left ");
                //     newEnemy.GetComponent<FlyingEnemy>().Flip();
                // }
                if(Random.Range(0,2) == 0){
                    Debug.Log("Hello direction gonna be left");
                    newEnemy.GetComponent<FlyingEnemy>().Flip();
                }
                spawnedEnemies.Add(newEnemy);
                enemiesSpawned++;
            }
        }
    }
}
