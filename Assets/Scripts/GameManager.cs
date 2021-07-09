using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    public static GameManager Instance;

    //las variables públicas son serializadas
    public int tiempo = 60;
    [SerializeField] public int dificultad = 1;
    [SerializeField] int score;
    public bool gameOver=false;
    public bool gameStandby=true;

    public int Score{
        get => score;
        set {
            score = value;
            UIManager.Instance.ActualizarUIScore(score);
            if (score % 1000 == 0)
            {
                dificultad++;
            }
        }
    }
    private void Awake() 
    {
        if (Instance==null)
        {
            Instance=this;
        }
    }

    private void Start() {
        UIManager.Instance.ShowPantallaInicio();
        StartCoroutine(ContadorRutina());
    }

    IEnumerator ContadorRutina(){
        
        while(tiempo>0){
            yield return new WaitForSeconds(1);
            tiempo--;
            UIManager.Instance.ActualizarUITiempo(tiempo);
        }

        gameOver=true;
        UIManager.Instance.ShowGameOverScreen();
    }
    public void PlayAgain(){
        LimpiarPantalla();
        Time.timeScale=1;
        UIManager.Instance.gameOverScreen.SetActive(false);
        StartCoroutine(ContadorRutina());
        // SceneManager.LoadScene("Game");
    }
    public void IniciarJuego(){
        gameStandby=false;
        // activar los componentes
        Time.timeScale=1;
        UIManager.Instance.startScreen.SetActive(false);
        // SceneManager.LoadScene("Game");
    }
    private void LimpiarPantalla(){
        var enemigos = GameObject.FindGameObjectsWithTag("Enemigo");
        foreach (var enemigo in enemigos)
        {
            Destroy(enemigo);
        }
        var powerups = GameObject.FindGameObjectsWithTag("PowerUp");
        foreach (var powerup in powerups)
        {
            Destroy(powerup);
        }
        var checks = GameObject.FindGameObjectsWithTag("Checkpoint");
        foreach (var check in checks)
        {
            Destroy(check);
        }

        // reiniciar tiempo
        tiempo=5;

        // reiniciar score
        Score = 0;
        
        // reubicar
        Player.Instance.transform.position=new Vector2(0,0);

        // reiniciar salud
        Player.Instance.Salud=5;
    }
}
