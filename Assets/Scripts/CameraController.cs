using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraController : MonoBehaviour
{
    CinemachineVirtualCamera vCam;
    CinemachineBasicMultiChannelPerlin noise;
    void Start()
    {
        vCam=GetComponent<CinemachineVirtualCamera>();
        noise=vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    public void Shake(float duracion=0.1f, float amplitud=1.5f, float frecuencia=20){
        StopAllCoroutines();
        StartCoroutine(AplicarRuido(duracion,amplitud,frecuencia));
    }
    IEnumerator AplicarRuido(float duracion, float amplitud, float frecuencia){
        noise.m_AmplitudeGain=amplitud;
        noise.m_FrequencyGain=frecuencia;
        yield return new WaitForSeconds(duracion);
        noise.m_AmplitudeGain=0;
        noise.m_FrequencyGain=0;
    }
}
