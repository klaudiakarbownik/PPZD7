using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public EnemyController enemyPrefab;
    public float spawnInterval = 5;
    public Transform[] spawnPoints;
    public bool isActive;
    private float _spawnDelayTimer = 3.0f;
    
    private void Update() {
        if (_spawnDelayTimer > 0) {
            _spawnDelayTimer -= Time.deltaTime;
            
            return;
        }
        
        EnemyController[] allEnemies = FindObjectsOfType<EnemyController>();
        
        if(allEnemies.Length >= 2) { 
            _spawnDelayTimer = 1;
            
            return;
        }
        
        _spawnDelayTimer = spawnInterval;
        
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        
        if (isActive)
        {
            Instantiate(enemyPrefab, spawnPoints[spawnPointIndex].position, Quaternion.identity);
        }
    }
}