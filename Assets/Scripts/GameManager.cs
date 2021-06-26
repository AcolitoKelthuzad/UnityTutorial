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
        StartCoroutine(ContadorRutina());
    }

    IEnumerator ContadorRutina(){
        
        while(tiempo>0){
            yield return new WaitForSeconds(1);
            tiempo--;
        }

        gameOver=true;
        UIManager.Instance.ShowGameOverScreen();
    }
    public void PlayAgain(){
        Time.timeScale=1;
        UIManager.Instance.gameOverScreen.SetActive(false);
        SceneManager.LoadScene("Game");
    }
}
