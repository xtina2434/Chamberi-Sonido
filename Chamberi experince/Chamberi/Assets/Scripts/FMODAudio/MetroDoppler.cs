using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MetroDoppler : MonoBehaviour
{
    private FMODUnity.StudioEventEmitter emitter;
    private EventInstance instance;
    private int reverbIndex = 1;

    // Aplica reverb de la zona del anden a la instancia del evento del metro
    void Start()
    {
        emitter = this.GetComponent<FMODUnity.StudioEventEmitter>();
        instance = emitter.EventInstance;
        instance.setReverbLevel(reverbIndex, 1.0f);
    }

    void Update()
    {
        // Actualiza la posicion 3D de la fuente de sonido (el metro se mueve)
        instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
    }
}
