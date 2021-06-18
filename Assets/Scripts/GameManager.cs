using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    public static GameManager Instance;
    [SerializeField] int tiempo = 30;
    [SerializeField] public int dificultad = 1;
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

        //Game over
    }
}
