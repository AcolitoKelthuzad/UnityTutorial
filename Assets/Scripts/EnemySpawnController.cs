using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    [SerializeField] GameObject[] enemigos;
    [Range(1,10)][SerializeField] float spawnRate=1;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnNuevoEnemigo());
    }

    IEnumerator SpawnNuevoEnemigo(){
        while (true)
        {
            //tiempo entre creación de enemigos
            yield return new WaitForSeconds(1/spawnRate);

            /*var random = Random.Range(0,enemigos.Length);
            Instantiate(enemigos[(int)random]);*/

            float random = Random.Range(0.0f,1.0f);

            if (random < GameManager.Instance.dificultad * 0.1f)
            {
                Instantiate(enemigos[0]);                
            }else
            {
                Instantiate(enemigos[1]);
            }
        }
    }
}
