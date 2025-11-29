
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    private List<EventInstance> eventInstances;
    private List<StudioEventEmitter> eventEmitters;

    public EventInstance ambienceCreepyEventInstance;
    public EventInstance tormentaEventInstance;

    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Se ha encontrado mas de un Audio Manager en la escena.");
        }
        instance = this;

        eventInstances = new List<EventInstance>();
        eventEmitters = new List<StudioEventEmitter>();
    }

    private void Start()
    {
        InitializeAmbience(FMODEvents.instance.ambience);
        InitializeTormenta(FMODEvents.instance.tormenta);
    }
    private void InitializeAmbience (EventReference ambienceEventReference)
    {
        ambienceCreepyEventInstance = CreateEventInstance(ambienceEventReference);
        ambienceCreepyEventInstance.start();
        ambienceCreepyEventInstance.setParameterByName("ambience_intensity", 0.0f);
    }
    private void InitializeTormenta(EventReference tormentaEventReference)
    {
        tormentaEventInstance = CreateEventInstance(tormentaEventReference);
        tormentaEventInstance.setParameterByName("volumen_tormenta", 1.0f);
        tormentaEventInstance.setParameterByName("intensidad_tormenta", 0.2f);
        tormentaEventInstance.setParameterByName("frecuencia_tormenta", 0.5f);
        tormentaEventInstance.start();
       
    }
    public void SetAmbienceParameter(string parameterName, float parameterValue)
    {
        ambienceCreepyEventInstance.setParameterByName(parameterName, parameterValue);
    }

    public void PlayOneShot(EventReference sound, Vector3 position) {

        RuntimeManager.PlayOneShot(sound, position);
    }
    
    public EventInstance CreateEventInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }
}
