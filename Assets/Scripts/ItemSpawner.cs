using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject checkpointPrefab;
    [SerializeField] int spawnDelay = 10;
    [SerializeField] float spawnRadio = 10;
    [SerializeField] GameObject[] powerUpPrefab;
    [SerializeField] int powerUpSpawnDelay=12;
    void Start()
    {
        StartCoroutine(SpawnCheckpointRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    IEnumerator SpawnCheckpointRoutine(){
        while (true)
        {
            //tiempo que tarda un checkpoint en aparecer
            yield return new WaitForSeconds(spawnDelay);
            //posicion en la que va a aparecer, en spawnRadio unidades alrededor del inicio
            Vector2 randomPosition = Random.insideUnitCircle * spawnRadio;
            Instantiate(checkpointPrefab, randomPosition, Quaternion.identity);   

        }
    }

    IEnumerator SpawnPowerUpRoutine(){
        while (true)
        {
            yield return new WaitForSeconds(powerUpSpawnDelay);
            Vector2 randomPosition = Random.insideUnitCircle * spawnRadio;
            int random= Random.Range(0,powerUpPrefab.Length);
            Instantiate(powerUpPrefab[random],randomPosition,Quaternion.identity);
        }
    }

}
