using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

// Gestiona los eventos de audio globales de la escena
// Implementa patron singleton para acceso global
public class AudioManager : MonoBehaviour
{
    // Instancia del evento de ambiente creepy
    public EventInstance ambienceCreepyEventInstance;

    // Instancia del evento de tormenta
    public EventInstance tormentaEventInstance;

    // Singleton de AudioManager
    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        // Asegurar que solo hay un AudioManager en la escena
        if(instance != null)
        {
            Debug.LogError("Se ha encontrado mas de un Audio Manager en la escena.");
        }
        instance = this;
    }

    private void Start()
    {
        InitializeAmbience(FMODEvents.instance.ambience);
        InitializeTormenta(FMODEvents.instance.tormenta);
    }

    // Inicializa el evento de ambiente creepy
    private void InitializeAmbience (EventReference ambienceEventReference)
    {
        // Crea la instancia
        ambienceCreepyEventInstance = CreateEventInstance(ambienceEventReference);
        // Empieza a reproducirse
        ambienceCreepyEventInstance.start();
        // Establece la intensidad a 0
        ambienceCreepyEventInstance.setParameterByName("ambience_intensity", 0.0f);
    }

    // Inicializa el evento de tormenta
    private void InitializeTormenta(EventReference tormentaEventReference)
    {
        // Crea la instancia
        tormentaEventInstance = CreateEventInstance(tormentaEventReference);
        // Empieza a reproducirse
        tormentaEventInstance.start();
        // Establece el volumen a 1
        tormentaEventInstance.setParameterByName("volumen_tormenta", 1.0f);
    }
    
    // Crea una instancia de un evento de FMOD y la devuelve
    public EventInstance CreateEventInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        return eventInstance;
    }
}
