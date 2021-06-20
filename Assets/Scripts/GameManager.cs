using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    public static GameManager Instance;

    //las variables públicas son serializadas
    public int tiempo = 60;
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
