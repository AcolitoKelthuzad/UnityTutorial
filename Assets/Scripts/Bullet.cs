using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 5;

    private void Start() 
    {
        //destruye a la bala después de 5 segundos
        Destroy(gameObject,5);
    }
    void Update()
    {
        transform.position+= transform.right * Time.deltaTime * speed;
    }

    //si la bala golpea un enemigo se le quita daño y se destruye
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemigo"))
        {
            collision.GetComponent<Enemy>().RecibirDanio();
            Destroy(gameObject);
        }
    }
}
