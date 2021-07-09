using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    float horizontal;
    float vertical;
    Vector3 moveDirection;
    public float velocidad= 3;
    // Start is called before the first frame update. solo se ejecuta una vez, al inicio
    [SerializeField] Transform aim;
    [SerializeField] Camera camera;
    [SerializeField] Transform bulletPrefab;
    public float cadencia=1;
    [SerializeField] int salud=5;
    [SerializeField] int invulnerableTime = 3;
    bool armaCargada = true;
    Vector2 facingDirection;
    bool powerShotEnabled = false;
    public bool invulnerable=false;
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer spriteRenderer;
    public int Salud{
        get => salud;
        set {
            salud = value;
            UIManager.Instance.ActualizarUISalud(salud);
        }
    }
    private void Awake() 
    {
        if (Instance==null)
        {
            Instance=this;
        }
    }
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
            //Instantiate(bulletPrefab,transform.position, targetRotation);
            Transform bulletClone= Instantiate(bulletPrefab,transform.position, targetRotation);
            if (powerShotEnabled)
            {
                bulletClone.GetComponent<Bullet>().powerShot = true;
            }
            StartCoroutine(RecargarArma());
            
        }

        anim.SetFloat("Velocidad",moveDirection.magnitude);
        if (aim.position.x > transform.position.x)
        {
            spriteRenderer.flipX=true;
        }else if(aim.position.x < transform.position.x)
        {
            spriteRenderer.flipX=false;
        }
    }
    
    IEnumerator RecargarArma(){
        yield return new WaitForSeconds(1/cadencia);
        armaCargada=true;
        
    }

    public void RecibirDanio(){
        if (invulnerable)
            return;

        Salud-- ;
        invulnerable=true;
        if (Salud<=0)
        {
            GameManager.Instance.gameOver=true;
            UIManager.Instance.ShowGameOverScreen();
        }
        StartCoroutine(Invulnerabilidad());
    }
    IEnumerator Invulnerabilidad(){
        yield return new WaitForSeconds(invulnerableTime);
        invulnerable=false;
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("PowerUp"))
        {
            switch (collision.GetComponent<PowerUp>().powerUpType)
            {
                case PowerUp.PowerUpType.FireRateIncrease:
                    cadencia++;
                    break;
                case PowerUp.PowerUpType.PowerShot:
                    powerShotEnabled = true;
                    // collision.GetComponent<Bullet>().durabilidad++;
                    break;
            }
            Destroy(collision.gameObject,0.1f);
        }
    }
}
