using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] Text saludText;
    [SerializeField] Text scoreText;
    [SerializeField] Text tiempoText;
    public GameObject gameOverScreen;
    [SerializeField] Text finalScore;

    private void Awake() {
        if (Instance==null)
        {
            Instance=this;
        }
    }
    public void ActualizarUIScore(int newScore){
        scoreText.text=newScore.ToString();
    }

    public void ActualizarUISalud(int newSalud){
        saludText.text=newSalud.ToString();
    }
    public void ActualizarUITiempo(int newTiempo){
        tiempoText.text=newTiempo.ToString();
    }
    public void ShowGameOverScreen(){
        Time.timeScale=0;
        gameOverScreen.SetActive(true);
        finalScore.text="SCORE: "+GameManager.Instance.Score;
    }
}
