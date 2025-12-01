using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
public class RataManager : MonoBehaviour
{
    private FMODUnity.StudioEventEmitter emitter;
    private EventInstance instance;
    private int reverbIndex = 1;

    // Aplica reverb de la zona del anden a la instancia del evento del sonido de la rata
    void Start()
    {
        emitter = this.GetComponent<FMODUnity.StudioEventEmitter>();
        instance = emitter.EventInstance;
        instance.setReverbLevel(reverbIndex, 1.0f);
    }
}
