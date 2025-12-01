using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaquinaManager : MonoBehaviour
{
    private FMODUnity.StudioEventEmitter emitter;
    private EventInstance instance;
    public int reverbIndex = 2;

    // Aplica reverb de la zona del tunel a la instancia del evento de gotas de agua
    void Start()
    {
        emitter = this.GetComponent<FMODUnity.StudioEventEmitter>();
        instance = emitter.EventInstance;
        instance.setReverbLevel(reverbIndex, 1.0f);
    }
}
