using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    Transform player;
    [SerializeField] int salud = 1;
    [SerializeField] float velocidad = 1;
    [SerializeField] int scorePoints = 100;
    private void Start()
    {
        player = FindObjectOfType<Player>().transform;
        GameObject[] spawnPoint = GameObject.FindGameObjectsWithTag("SpawnPoint");
        int randomSpawnPoint = Random.Range(0, spawnPoint.Length);
        transform.position = spawnPoint[randomSpawnPoint].transform.position;
    }
    private void Update() {
        //transform es el enemigo
        Vector2 direction=player.position - transform.position;
        // direction.Normalize();
        transform.position += (Vector3)direction.normalized * Time.deltaTime * velocidad;
    }
    public void RecibirDanio(){
        salud--;
        if (salud<=0)
        {
            GameManager.Instance.Score +=scorePoints;
            Destroy(gameObject);
        }
    }

    //Si el enemigo golpea al jugador, le hace daño
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Jugador") && !collision.GetComponent<Player>().invulnerable)
        {
            collision.GetComponent<Player>().RecibirDanio();
            
        }
    }
}
