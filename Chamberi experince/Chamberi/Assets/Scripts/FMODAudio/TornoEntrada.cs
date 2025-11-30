using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class TornoEntrada : MonoBehaviour
{
    bool hasEnter = false;
    private FMODUnity.StudioEventEmitter emitter;
    private EventInstance instance;

    private int reverbIndex = 2;

    void Start()
    {
        emitter = this.GetComponent<FMODUnity.StudioEventEmitter>();
        instance = emitter.EventInstance;
        instance.setReverbLevel(reverbIndex, 1.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            hasEnter = !hasEnter;
        }
        if (hasEnter)
        {
            emitter.Play();
        }
    }
}
