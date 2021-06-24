using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agua : MonoBehaviour
{
    float velocidadOriginal;
    Player player;
    [SerializeField]float ratioReduccionVelocidad = 0.5f;
    void Start()
    {
        player=FindObjectOfType<Player>();
        velocidadOriginal=player.velocidad;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Jugador"))
        {
            player.velocidad *= ratioReduccionVelocidad;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Jugador"))
        {
            player.velocidad=velocidadOriginal;
        }
    }
}
