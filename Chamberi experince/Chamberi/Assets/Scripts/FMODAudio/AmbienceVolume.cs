using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controla la intensidad del ambiente creepy segun la posicion del jugador.
// Aumenta la intensidad cuando el jugador esta dentro de la estacion
// y reduce cuando esta fuera
public class AmbienceVolume : MonoBehaviour
{
    // Nombre del parametro de FMOD que controla la intensidad
    private string intensityParam = "ambience_intensity";

    // Valor maximo que puede alcanzar la intensidad
    private float maxIntensity = 1.0f;
    // Velocidad de transicion
    private float fade = 2.0f;
    // Distancia maxima a partir de la cual la intensidad llega al maximo
    private float maxDistance = 50f;
    // Valor actual del parametro intensidad
    private float currentIntensity = 0.0f;

    // Indica si el jugador esta dentro de la estacion de metro
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
        // Si el jugador esta dentro de la estacion
        if (playerInside)
        {
            // Vector desde la entrada hasta el jugador
            Vector3 toPlayer = player.position - transform.position;

            // Comprueba si el jugador esta delante o detras del objeto de entrada
            float signed = Vector3.Dot(toPlayer, transform.forward);

            // Obtener el valor actual del parametro
            AudioManager.instance.ambienceCreepyEventInstance.getParameterByName(intensityParam, out currentIntensity);
          
            // Si esta detras (fuera de la estacion)
            if (signed <= 0f)
            {
                // La intensidad es 0
                float target = 0f;
                currentIntensity = Mathf.Lerp(currentIntensity, target, Time.deltaTime * fade);
               
            }
            else
            {
                // Normaliza la distancia entre 0(cerca) y 1(lejos)
                float target = Mathf.Clamp01(signed / maxDistance);

                // Suaviza la transicion de 'currentIntensity' hacia el valor objetivo
                currentIntensity = Mathf.Lerp(currentIntensity, target,Time.deltaTime * fade);
            }

            // Actualiza el parametro intensidad en FMOD
            AudioManager.instance.ambienceCreepyEventInstance.setParameterByName(intensityParam, currentIntensity);
        }
        else
        {
            // Si el jugador esta fuera de la estacion, la intensidad se va bajando hacia el valor 0
            currentIntensity = Mathf.Lerp(currentIntensity, 0f, Time.deltaTime * fade);

            // Actualiza el parametro intensidad en FMOD
            AudioManager.instance.ambienceCreepyEventInstance.setParameterByName(intensityParam, currentIntensity);
        }
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
