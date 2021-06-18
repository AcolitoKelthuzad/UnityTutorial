using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float horizontal;
    float vertical;
    Vector3 moveDirection;
    [SerializeField] float velocidad= 3;
    // Start is called before the first frame update. solo se ejecuta una vez, al inicio
    [SerializeField] Transform aim;
    [SerializeField] Camera camera;
    [SerializeField] Transform bulletPrefab;
    [SerializeField] float cadencia=1;
    [SerializeField] int salud=5;
    bool armaCargada = true;
    Vector2 facingDirection;
    void Start()
    {
        print("Hola, soy un player visible... vamo");
    }

    // Update se utiliza en cada frame, es decir mientras el programa esté corriendo
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        moveDirection.x = horizontal;
        moveDirection.y = vertical;

        transform.position += moveDirection * Time.deltaTime * velocidad;

        //Movimiento de la mira

        facingDirection = camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        aim.position = transform.position + (Vector3)facingDirection.normalized;

        if(Input.GetMouseButton(0) && armaCargada){
            armaCargada = false;
            float angle= Mathf.Atan2(facingDirection.y,facingDirection.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Instantiate(bulletPrefab,transform.position, targetRotation);
            StartCoroutine(RecargarArma());
            
        }
    }
    
    IEnumerator RecargarArma(){
        yield return new WaitForSeconds(1/cadencia);
        armaCargada=true;
        
    }

    public void RecibirDanio(){
        salud-- ;
        if (salud<=0)
        {
            //game over
        }
    }
}