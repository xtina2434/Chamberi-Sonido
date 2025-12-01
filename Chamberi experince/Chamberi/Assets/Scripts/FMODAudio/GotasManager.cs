using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotasManager : MonoBehaviour
{
    private FMODUnity.StudioEventEmitter emitter;
    private EventInstance instance;
    private int reverbIndex = 0;

    // Aplica reverb de la zona del tunel a la instancia del evento de gotas de agua
    void Start()
    {
        emitter = this.GetComponent<FMODUnity.StudioEventEmitter>();
        instance = emitter.EventInstance;
        instance.setReverbLevel(reverbIndex, 1.0f);
    }
}
