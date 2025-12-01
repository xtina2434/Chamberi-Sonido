using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Este script controla los paramatros para el evento de tormenta

public class AmbienceStorm : MonoBehaviour
{
    // Nombre de los parametros en FMOD
    private string intensityParam = "intensidad_tormenta";
    private string frequencyParam = "frecuencia_tormenta";
    private string obsParam = "obstruccion_tormenta";
    private string oclParam = "oclusion_tormenta";

    // Velocidad de variacion de intensidad y frecuencia de la tormenta
    private float intensitySpeed = 0.05f;
    private float frequencySpeed = 0.5f;

    // Velocidad de la transicion de obstruccion y oclusion
    private float fade = 0.5f;
    // Valor interpolado actual
    private float currentValue = 0f;
    // Distancia maxima para atenuacion
    private float maxDistance = 30f;

    // Bandera que indica si el jugador esta fuera o dentro de la estacion de metro
    private bool playerInside = false;

    // Referencia al jugador
    private Transform player;

    void Start()
    {
        // Obtener transform del jugador
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        // La intensidad oscila en el tiempo entre 0-1
        float intensityValue = Mathf.PingPong(Time.time * intensitySpeed, 1f);

        // La frecuencia depende de la intensidad (alta cuando intensidad >= 0.5)
        float frequencyValue = 0.0f;

        if (intensityValue >= 0.5f)
        {
            // target es el valor que representa 'intensityValue' entre 0.5 y 1  
            float target = Mathf.InverseLerp(0.5f, 1f, intensityValue);  

            // La frecuencia oscila segun la intensidad de la tormenta
            frequencyValue = Mathf.PingPong(Time.time * frequencySpeed, 1f) * target;
        }

        // Aplicar la intensidad y frecuencia al evento FMOD
        AudioManager.instance.tormentaEventInstance.setParameterByName(intensityParam, intensityValue);
        AudioManager.instance.tormentaEventInstance.setParameterByName(frequencyParam, frequencyValue);

        // Si el jugador esta dentro de la estacion
        if (playerInside)
        {
            // Calcular la obstruccion y oclusion segun la distancia a la entrada de la estacion de metro
            float distance = Vector3.Distance(player.position, transform.position);

            // Normaliza la distancia entre 0(cerca) y 1(lejos)
            float t = Mathf.Clamp01(distance / maxDistance);

            // Suaviza la transicion de 'currentValue' hacia el valor objetivo
            currentValue = Mathf.Lerp(currentValue, t, Time.deltaTime * fade);
        }
        else
        {
            // Cuando esta fuera de la estacion, la obstruccion y oclusion vuelven hacia el valor 0
            currentValue = Mathf.Lerp(currentValue, 0f, Time.deltaTime * fade);
        }

        // Aplicar la obstruccion y oclusion al evento FMOD
        AudioManager.instance.tormentaEventInstance.setParameterByName(obsParam, currentValue);
        AudioManager.instance.tormentaEventInstance.setParameterByName(oclParam, currentValue);
    }


    private void OnTriggerEnter(Collider other)
    {
        // Detecta cuando el jugador esta dentro de la estacion de metro
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }
}
