using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
public class RataManager : MonoBehaviour
{
    private FMODUnity.StudioEventEmitter emitter;
    private EventInstance instance;
    private int reverbIndex = 1;

    void Start()
    {
        emitter = this.GetComponent<FMODUnity.StudioEventEmitter>();
        instance = emitter.EventInstance;
        instance.setReverbLevel(reverbIndex, 1.0f);
    }
}
